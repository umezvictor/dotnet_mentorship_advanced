using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
//IS4 refers to IdentityServer4
var authorityUrl = builder.Configuration["Authentication:AuthorityUrl"];
builder.Services
	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer("IS4", o =>
	{
		o.Authority = authorityUrl;
	});

builder.Services
	.AddOcelot(builder.Configuration)
	.AddCacheManager(x => x.WithDictionaryHandle());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
var catalogApiSwaggerUrl = builder.Configuration["SwaggerUrls:CatalogAPI"];
var cartApiSwaggerUrl = builder.Configuration["SwaggerUrls:CartAPI"];

app.Map("/swagger/v1/swagger.json", builder =>
	builder.Run(async context =>
	{
		using var httpClient = new HttpClient();
		var json = await httpClient.GetStringAsync(catalogApiSwaggerUrl);
		context.Response.ContentType = "application/json";
		await context.Response.WriteAsync(json);
	}));

app.Map("/swagger/v2/swagger.json", builder =>
	builder.Run(async context =>
	{
		using var httpClient = new HttpClient();
		var json = await httpClient.GetStringAsync(cartApiSwaggerUrl);
		context.Response.ContentType = "application/json";
		await context.Response.WriteAsync(json);
	}));

app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API");
	c.SwaggerEndpoint("/swagger/v2/swagger.json", "Cart API");
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await app.UseOcelot();

app.Run();
