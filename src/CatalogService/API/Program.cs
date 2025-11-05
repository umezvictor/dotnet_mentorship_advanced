using API;
using API.Services;
using BLL;
using BLL.Abstractions;
using DAL;
using Serilog;
using Shared.Constants;

var builder = WebApplication.CreateBuilder(args);

var _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddPresentationLayer(_config)
    .AddBusinessLogicLayer()
    .AddDataAccessLayer(_config);


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILinkService, LinkService>();
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

namespace CatalogService
{
    public partial class Program { }
}