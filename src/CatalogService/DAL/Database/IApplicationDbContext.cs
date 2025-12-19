using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
namespace DAL.Database;

public interface IApplicationDbContext
{
	public DbSet<Product> Products { get; set; }
	public DbSet<Category> Category { get; set; }
	public DbSet<Outbox> Outbox { get; set; }
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	DatabaseFacade Database { get; }
}