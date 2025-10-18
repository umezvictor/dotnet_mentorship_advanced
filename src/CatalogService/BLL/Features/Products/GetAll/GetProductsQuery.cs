using AutoMapper;
using BLL.Abstractions;
using MediatR;
using Shared;
using Shared.Dto;

namespace BLL.Features.Products.GetAll;

public sealed class GetProductsQuery : IRequest<Response<List<ProductDto>>>
{
}
public class GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper) :
    IRequestHandler<GetProductsQuery, Response<List<ProductDto>>>
{
    public async Task<Response<List<ProductDto>>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var categories = await productRepository.GetAllProductsAsync(cancellationToken);
        if (!categories.Any())
            return new Response<List<ProductDto>>(new List<ProductDto>(), ResponseMessage.NotItemsPresent);

        return new Response<List<ProductDto>>(mapper.Map<List<ProductDto>>(categories), ResponseMessage.Success);
    }
}