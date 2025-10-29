using BLL.Abstractions;
using MediatR;
using Shared;
using Shared.Dto;

namespace BLL.Features.Products.GetAll;

public sealed class GetProductsQuery : IRequest<Response<PaginatedResponse<List<ProductDto>>>>
{
    public int CategoryId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

}
public class GetProductsQueryHandler(IProductRepository productRepository) :
    IRequestHandler<GetProductsQuery, Response<PaginatedResponse<List<ProductDto>>>>
{
    public async Task<Response<PaginatedResponse<List<ProductDto>>>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var paginatedResponse = await productRepository.GetProductsByCategoryIdAsync
            (query.CategoryId, query.PageNumber, query.PageSize, cancellationToken);

        if (paginatedResponse is null && !paginatedResponse.Data.Any())
            return new Response<PaginatedResponse<List<ProductDto>>>(new PaginatedResponse<List<ProductDto>>(), ResponseMessage.NotItemsPresent);

        return new Response<PaginatedResponse<List<ProductDto>>>(paginatedResponse, ResponseMessage.Success);
    }



}