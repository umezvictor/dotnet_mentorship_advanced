using AutoMapper;
using BLL.Abstractions;
using Domain.Entities;
using MediatR;
using Shared;

namespace BLL.Features.Products.Add;
public sealed class AddProductCommand : IRequest<Response<long>>
{
    public string Name { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public int CategoryId { get; set; }

}

public class AddProductCommandHandler(IProductRepository productRepository, IMapper mapper) :
    IRequestHandler<AddProductCommand, Response<long>>
{
    public async Task<Response<long>> Handle(AddProductCommand command, CancellationToken cancellationToken)
    {
        var createdProductId = await productRepository.CreateAsync(mapper.Map<Product>(command), cancellationToken);
        return new Response<long>(createdProductId, ResponseMessage.ProductAdded);
    }
}
