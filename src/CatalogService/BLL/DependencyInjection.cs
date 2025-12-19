using System.Reflection;
using BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BLL;
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
		services.AddScoped<ICategoryService, CategoryService>();
		services.AddScoped<IProductService, ProductService>();
		return services;
	}


}
