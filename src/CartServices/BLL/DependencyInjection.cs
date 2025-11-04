using BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CartServices.BLL;
public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        return services
            .AddPackages();
    }

    public static IServiceCollection AddPackages(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<ICartService, CartService>();
        return services;
    }


}
