using API.Infrastructure;
using CartService.BLL;
using CartService.DAL;
using Microsoft.AspNetCore.Http.Features;
using System.Diagnostics;


var builder = WebApplication.CreateBuilder(args);

var _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();



// Add services to the container.

builder.Services.AddBusinessLogicLayer()
    .AddDataAccessLayer(_config);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance =
            $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

        Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
    };
});


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

namespace CartService
{
    public partial class Program;
}