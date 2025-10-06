using CartService.BLL.Abstractions;
using CartService.DAL.Database.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CartService.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddMongoDbClient(configuration)
            .AddCartRepository();

    }

    private static IServiceCollection AddMongoDbClient(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString = configuration["MONGODB_CONNECTION_STRING"];

        var mongoUrl = new MongoUrl(dbConnectionString);
        var mongoClient = new MongoClient(mongoUrl);

        services.AddSingleton<IMongoClient>(mongoClient);

        services.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(mongoUrl.DatabaseName);
        });

        return services;
    }



    private static IServiceCollection AddCartRepository(this IServiceCollection services)
    {
        services.AddScoped<ICartRepository, CartRepository>();
        return services;
    }
}
