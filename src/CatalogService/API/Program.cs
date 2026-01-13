using API;
using API.Middleware;
using API.Services;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using BLL;
using BLL.Abstractions;
using DAL;
using DAL.Database;
using OpenTelemetry.Trace;
using RabbitMQ;
using Serilog;
using Shared.Constants;

var builder = WebApplication.CreateBuilder(args);
var _config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();
var applicationInsightsUrl = builder.Configuration["ApplicationInsightsUrl"];

Log.Logger = new LoggerConfiguration()
	.WriteTo.ApplicationInsights(
		applicationInsightsUrl,
		TelemetryConverter.Traces)
	.CreateLogger();

builder.Services.AddPresentationLayer(_config)
	.AddBusinessLogicLayer()
	.AddDataAccessLayer(_config);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILinkService, LinkService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRabbitMqClient, RabbitMqClient>();
builder.Host.UseSerilog();


builder.Services.AddOpenTelemetry()
	.UseAzureMonitor(options =>
	{
		options.ConnectionString = applicationInsightsUrl;
	})
	.WithTracing(tracing => tracing
		.AddAspNetCoreInstrumentation(options =>
		{
			// This will add the correlation_id tag to all traces, if present in the headers
			options.EnrichWithHttpRequest = (activity, httpRequest) =>
			{
				if (httpRequest.Headers.TryGetValue(AppConstants.CorrelationIdHeader, out var corrId))
				{
					activity.SetTag(AppConstants.CorrelationIdTag, corrId.ToString());
				}
			};
		})
		.AddSource(AppConstants.MessageBroker)
	);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	await MigrationManager.ApplyMigrationsAsync(app.Services);
}
app.UseMiddleware<CorrelationIdMiddleware>();

app.UseCors(AppConstants.CorsPolicy);
app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();

namespace CatalogService
{
	public partial class Program { }
}