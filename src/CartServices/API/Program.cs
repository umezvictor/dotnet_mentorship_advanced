using API;
using CartServices.BLL;
using CartServices.DAL;
using Microsoft.OpenApi.Models;
using Serilog;
using Shared.Constants;


var builder = WebApplication.CreateBuilder(args);

var _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// register layers
builder.Services.AddPresentationLayer(_config)
    .AddBusinessLogicLayer()
    .AddDataAccessLayer(_config);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "My API v1" });
    options.SwaggerDoc("v2", new OpenApiInfo { Version = "v2", Title = "My API v2" });
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // app.UseSwaggerUI();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "My API v2");
    });
}

app.UseCors(AppConstants.CorsPolicy);
app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseAuthorization();

app.MapControllers();

app.Run();

namespace CartService
{
    public partial class Program;
}