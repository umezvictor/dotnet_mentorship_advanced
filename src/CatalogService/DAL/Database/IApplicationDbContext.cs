using DAL.Entities;
using Microsoft.EntityFrameworkCore;
namespace DAL.Database;

public interface IApplicationDbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Category { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}