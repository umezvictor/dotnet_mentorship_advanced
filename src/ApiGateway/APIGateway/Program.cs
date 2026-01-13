using APIGateway;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

UrlSettings urlSettings = new UrlSettings();
builder.Configuration.GetSection("UrlSettings").Bind(urlSettings);
//IS4 refers to IdentityServer4
builder.Services
	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer("IS4", o =>
	{
		o.Authority = urlSettings.AuthorityUrl;
	});

builder.Services
	.AddOcelot(builder.Configuration)
	.AddCacheManager(x => x.WithDictionaryHandle());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// This service will collect and send telemetry data to Azure Monitor.
builder.Services.AddOpenTelemetry().UseAzureMonitor(options =>
{
	options.ConnectionString = urlSettings.ApplicationInsightUrl;
});

builder.Services.AddHttpClient("swagger");
builder.Services.AddScoped<IApiClient, ApiClient>();

var app = builder.Build();

app.Map("/swagger/v1/swagger.json", builder =>
	builder.Run(async context =>
	{
		var apiClient = context.RequestServices.GetRequiredService<IApiClient>();
		var json = await apiClient.GetAsync(urlSettings.CatalogApiSwaggerUrl);
		context.Response.ContentType = "application/json";
		await context.Response.WriteAsync(json);
	}));

app.Map("/swagger/v2/swagger.json", builder =>
	builder.Run(async context =>
	{
		var apiClient = context.RequestServices.GetRequiredService<IApiClient>();
		var json = await apiClient.GetAsync(urlSettings.CartApiSwaggerUrl);
		context.Response.ContentType = "application/json";
		await context.Response.WriteAsync(json);
	}));

app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API");
	c.SwaggerEndpoint("/swagger/v2/swagger.json", "Cart API");
});

app.UseMiddleware<CorrelationIdMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await app.UseOcelot();

app.Run();
