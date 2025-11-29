using API.Infrastructure;
using API.Jobs;
using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using Shared.Constants;
using System.Diagnostics;
using System.Threading.RateLimiting;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services, IConfiguration configuration) =>
          services
        .AddSecurity(configuration)
          .AddServices(configuration)
          .AddQuartzSetup();
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRazorPages();

        services.AddCors(o => o.AddPolicy(AppConstants.CorsPolicy, builder =>
        {
            builder.WithOrigins("*").AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
        }));

        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance =
                    $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
            };
        });

        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.AddPolicy(AppConstants.RateLimitingPolicy, httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 20,
                        Window = TimeSpan.FromMinutes(1),
                    }));
        });


        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
            options.ReportApiVersions = true;
        })
        .AddMvc(options =>
        {
            options.Conventions.Add(new VersionByNamespaceConvention());
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;

        });
        return services;
    }

    public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication("Bearer")
           .AddJwtBearer("Bearer", options =>
           {
               options.Authority = configuration["JwtSettings:Issuer"];

               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateAudience = false,
                   RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                   NameClaimType = "sub"

               };
           });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "cartApi");
            });


            options.AddPolicy("ManagerOrCustomerPolicy", policy =>
            {
                policy.RequireAssertion(context =>
                    (context.User.IsInRole("Manager") &&
                         context.User.HasClaim("permission", "Read") ||
                         context.User.HasClaim("permission", "Create") ||
                         context.User.HasClaim("permission", "Update") ||
                         context.User.HasClaim("permission", "Delete")
                    )
                    ||
                    (context.User.IsInRole("StoreCustomer") &&
                         context.User.HasClaim("permission", "Read")
                    )
                );
            });
        });

        return services;
    }

    public static IServiceCollection AddQuartzSetup(this IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {

            var jobKey = new JobKey(nameof(RabbitMqListenerJob));
            configure
                .AddJob<RabbitMqListenerJob>(jobKey)
                .AddTrigger(
                    trigger => trigger.ForJob(jobKey)
                    .WithSimpleSchedule(
                        schedule => schedule.WithIntervalInSeconds(10).RepeatForever()));

        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        return services;
    }
}

