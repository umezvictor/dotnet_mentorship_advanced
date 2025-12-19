using DAL.Database;
using DAL.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL;
public static class DependencyInjection
{
	public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration) =>
		services.AddDatabase(configuration);

	private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<ApplicationDbContext>(options =>
		options.UseSqlServer(
			configuration.GetConnectionString("DefaultConnection"),
			b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

		services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<ICategoryRepository, CategoryRepository>();

		return services;
	}


}