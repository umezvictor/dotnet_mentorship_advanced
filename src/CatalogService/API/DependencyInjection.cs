using API.Infrastructure;
using API.Jobs;
using Microsoft.AspNetCore.Http.Features;
using Quartz;
using Shared.Constants;
using System.Diagnostics;
using System.Threading.RateLimiting;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services, IConfiguration configuration) =>
           services
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

        return services;
    }


    public static IServiceCollection AddQuartzSetup(this IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {

            var jobKey = new JobKey(nameof(RabbitMQPublisherJob));
            configure
                .AddJob<RabbitMQPublisherJob>(jobKey)
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