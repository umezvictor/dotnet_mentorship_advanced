using API;
using CartService.BLL;
using CartService.DAL;
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
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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