using DAL.Entities;
using Shared;
using Shared.Dto;

namespace DAL.Database.Repository;
public interface IProductRepository
{
    Task<long> CreateAsync(Product product, CancellationToken cancellationToken);
    Task DeleteAsync(Product product, CancellationToken cancellationToken);
    Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken);
    Task<Product?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken);
    Task<PaginatedResponse<List<ProductDto>>> GetProductsByCategoryIdAsync(int categoryId, int pageNumber, int pageSize, CancellationToken cancellationToken);

}
