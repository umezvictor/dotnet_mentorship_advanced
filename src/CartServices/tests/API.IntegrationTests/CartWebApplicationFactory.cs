using CartService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Mongo2Go;
using MongoDB.Driver;
using Serilog;
using System.Text;

namespace API.IntegrationTests;

public class CartWebApplicationFactory : WebApplicationFactory<Program>
{

    private MongoDbRunner? _mongoRunner;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        _mongoRunner = MongoDbRunner.Start();

        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                ["MONGODB_CONNECTION_STRING"] = _mongoRunner.ConnectionString,
                ["MONGODB_DATABASE"] = "TestCartDb"
            };
            configBuilder.AddInMemoryCollection(inMemorySettings);
        });

        builder.ConfigureServices(services =>
        {
            var clientDescriptors = services.Where(d => d.ServiceType == typeof(IMongoClient)).ToList();
            foreach (var descriptor in clientDescriptors)
                services.Remove(descriptor);

            var dbDescriptors = services.Where(d => d.ServiceType == typeof(IMongoDatabase)).ToList();
            foreach (var descriptor in dbDescriptors)
                services.Remove(descriptor);


            var mongoClient = new MongoClient(_mongoRunner.ConnectionString);
            services.AddSingleton<IMongoClient>(mongoClient);
            services.AddScoped<IMongoDatabase>(sp =>
                sp.GetRequiredService<IMongoClient>().GetDatabase("TestCartDb"));


        });

        //Override JWT Authentication for Integration Tests
        builder.ConfigureTestServices(services =>
        {
            services.PostConfigureAll<JwtBearerOptions>(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://localhost:5001",
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("jWnZr4u7x!A%D*G-JaNdRgUkXp2s5v8y")
                    ),
                    ValidateLifetime = false,
                    RoleClaimType = "role",
                    NameClaimType = "sub"
                };
            });
        });

    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _mongoRunner?.Dispose();
        Log.CloseAndFlush();
    }

}



