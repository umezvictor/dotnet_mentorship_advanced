using Shared;
using Shared.Dto;

namespace BLL.Services;
public interface IProductService
{
    Task<Response<long>> AddProductAsync(AddProductRequest request, CancellationToken cancellationToken);
    Task<Response<string>> DeleteProductAsync(DeleteProductRequest request, CancellationToken cancellationToken);
    Task<Response<ProductDto>> GetProductByIdAsync(long id, CancellationToken cancellationToken);
    Task<Response<PaginatedResponse<List<ProductDto>>>> GetProductsByCategoryIdAsync(GetProductsQuery query, CancellationToken cancellationToken);
    Task<Response<string>> UpdateProductAsync(UpdateProductRequest request, CancellationToken cancellationToken);
}