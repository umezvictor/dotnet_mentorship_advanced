using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Database;
public static class MigrationManager
{
	public static async Task ApplyMigrationsAsync(IServiceProvider serviceProvider)
	{
		using var scope = serviceProvider.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
		try
		{
			var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

			if (pendingMigrations.Any())
			{
				await context.Database.MigrateAsync();
			}
		}
		catch (Exception ex)
		{
			throw new ApplicationException($"An error occurred while applying migrations. {ex.Message} ");
		}
	}
}