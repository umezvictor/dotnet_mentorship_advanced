using Domain.Entities;

namespace BLL.Abstractions;
public interface IProductRepository
{
    Task<long> CreateAsync(Product product, CancellationToken cancellationToken);
    Task DeleteAsync(Product product, CancellationToken cancellationToken);
    Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken);
    Task<Product?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken);
}
