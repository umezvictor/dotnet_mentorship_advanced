using API;
using CartServices.BLL;
using CartServices.DAL;
using RabbitMQ;
using Serilog;
using Shared.Constants;
using System.Reflection;


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



