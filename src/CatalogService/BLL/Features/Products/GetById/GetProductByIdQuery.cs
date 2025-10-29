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
public class GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper,
    ILinkService linkService) :
    IRequestHandler<GetProductByIdQuery, Response<ProductDto>>
{
    public async Task<Response<ProductDto>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(query.Id, cancellationToken);
        if (product is null)
            return new Response<ProductDto>(ResponseMessage.ProductNotFound, false);

        ProductDto productDto = mapper.Map<ProductDto>(product);
        AddLinksForProduct(productDto);
        return new Response<ProductDto>(productDto, ResponseMessage.Success);
    }

    private void AddLinksForProduct(ProductDto productDto)
    {
        productDto.Links.Add(linkService.GenerateLinks(
               "GetProduct",
               new { id = productDto.Id },
               "self",
               "GET"));

        productDto.Links.Add(linkService.GenerateLinks(
              "DeleteProduct",
              new { id = productDto.Id },
              "delete-product",
              "DELETE"));

        productDto.Links.Add(linkService.GenerateLinks(
              "UpdateProduct",
              new { id = productDto.Id },
              "update-product",
              "PUT"));

    }
}