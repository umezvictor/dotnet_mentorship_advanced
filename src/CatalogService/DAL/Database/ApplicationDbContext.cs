using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DAL.Database;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
	public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base( options )
	{
	}

	public DbSet<Product> Products { get; set; }
	public DbSet<Category> Category { get; set; }
	public DbSet<Outbox> Outbox { get; set; }
	public new DatabaseFacade Database => base.Database;
	protected override void OnModelCreating (ModelBuilder builder)
	{

		base.OnModelCreating( builder );
		builder.ApplyConfigurationsFromAssembly( typeof( ApplicationDbContext ).Assembly );
	}

	public override Task<int> SaveChangesAsync (CancellationToken cancellationToken = default)
	{
		var entries = ChangeTracker.Entries<AuditableEntity>();
		foreach (var entry in entries)
		{
			if (entry.State == EntityState.Added)
			{
				entry.Entity.CreatedAt = DateTime.UtcNow;
			}
			else if (entry.State == EntityState.Modified)
			{
				entry.Entity.UpdatedAt = DateTime.UtcNow;
			}
		}
		return base.SaveChangesAsync( cancellationToken );
	}
}

