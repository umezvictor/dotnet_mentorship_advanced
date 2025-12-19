using API;
using API.Services;
using BLL;
using BLL.Abstractions;
using DAL;
using DAL.Database;
using RabbitMQ;
using Serilog;
using Shared.Constants;

var builder = WebApplication.CreateBuilder( args );

var _config = new ConfigurationBuilder()
				.AddJsonFile( "appsettings.json" )
				.Build();

Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration( builder.Configuration )
	.CreateLogger();

builder.Services.AddPresentationLayer( _config )
	.AddBusinessLogicLayer()
	.AddDataAccessLayer( _config );


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILinkService, LinkService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRabbitMqClient, RabbitMqClient>();
builder.Host.UseSerilog();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	await MigrationManager.ApplyMigrationsAsync( app.Services );
}

app.UseCors( AppConstants.CorsPolicy );
app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();

namespace CatalogService
{
	public partial class Program { }
}