using System.Reflection;
using BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CartServices.BLL;
public static class DependencyInjection
{
	public static IServiceCollection AddBusinessLogicLayer (this IServiceCollection services)
	{
		return services
			.AddPackages();
	}

	public static IServiceCollection AddPackages (this IServiceCollection services)
	{
		services.AddAutoMapper( Assembly.GetExecutingAssembly() );
		services.AddScoped<ICartService, CartService>();
		return services;
	}


}
