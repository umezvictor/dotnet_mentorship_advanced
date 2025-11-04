using API.Infrastructure;
using Asp.Versioning;
using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.Http.Features;
using Shared.Constants;
using System.Diagnostics;
using System.Threading.RateLimiting;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services, IConfiguration configuration)
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
            //limit api calls that can be made from user's ip address
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
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;

            options.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("version"),
                new HeaderApiVersionReader("X-API-Version"),
                new MediaTypeApiVersionReader("version")
            );

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
}

