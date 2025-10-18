using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace BLL.Abstractions;

public interface IApplicationDbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Category { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}