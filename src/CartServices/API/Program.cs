using System.Reflection;
using API;
using API.Middleware;
using CartServices.BLL;
using CartServices.DAL;
using RabbitMQ;
using Serilog;
using Shared.Constants;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
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

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<AccessTokenLoggingMiddleware>();

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



