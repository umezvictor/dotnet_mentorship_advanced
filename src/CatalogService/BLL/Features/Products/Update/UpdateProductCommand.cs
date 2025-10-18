using AutoMapper;
using BLL.Abstractions;
using MediatR;
using Shared;

namespace BLL.Features.Products.Update;
public sealed class UpdateProductCommand : IRequest<Response<string>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Amount { get; set; }

}

public class UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper) :
    IRequestHandler<UpdateProductCommand, Response<string>>
{

    public async Task<Response<string>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(command.Id, cancellationToken);
        if (product is null)
        {
            return new Response<string>(ResponseMessage.ProductNotFound, false);
        }

        await productRepository.UpdateAsync(mapper.Map(command, product), cancellationToken);
        return new Response<string>(ResponseMessage.ProductUpdated, true);
    }
}
