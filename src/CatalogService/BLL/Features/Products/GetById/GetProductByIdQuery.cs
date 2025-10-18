using AutoMapper;
using BLL.Abstractions;
using MediatR;
using Shared;
using Shared.Dto;
namespace BLL.Features.Products.GetById;

public sealed class GetProductByIdQuery : IRequest<Response<ProductDto>>
{
    public int Id { get; set; }
}
public class GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper) :
    IRequestHandler<GetProductByIdQuery, Response<ProductDto>>
{
    public async Task<Response<ProductDto>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(query.Id, cancellationToken);
        if (product is null)
            return new Response<ProductDto>(ResponseMessage.ProductNotFound, false);

        return new Response<ProductDto>(mapper.Map<ProductDto>(product), ResponseMessage.Success);
    }
}