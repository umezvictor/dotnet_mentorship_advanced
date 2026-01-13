using System.Reflection;
using API;
using API.Middleware;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using CartServices.BLL;
using CartServices.DAL;
using OpenTelemetry.Trace;
using RabbitMQ;
using Serilog;
using Shared.Constants;


var builder = WebApplication.CreateBuilder(args);
var applicationInsightsUrl = builder.Configuration["ApplicationInsightsUrl"];

Log.Logger = new LoggerConfiguration()
	.WriteTo.ApplicationInsights(
		applicationInsightsUrl,
		TelemetryConverter.Traces)
	.CreateLogger();

// register layers
builder.Services.AddPresentationLayer(builder.Configuration)
	.AddBusinessLogicLayer()
	.AddDataAccessLayer(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
		$"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

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
				if (httpRequest.Headers.TryGetValue(AppConstants.CorrelationIdHeader, out var correlationId))
				{
					activity.SetTag(AppConstants.CorrelationIdTag, correlationId.ToString());
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
}
app.UseMiddleware<AccessTokenLoggingMiddleware>();
app.UseMiddleware<CorrelationIdMiddleware>();

app.UseCors(AppConstants.CorsPolicy);
app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();

namespace CartService
{
	public partial class Program;
}



