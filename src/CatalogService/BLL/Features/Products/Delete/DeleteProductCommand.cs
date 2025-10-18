using BLL.Abstractions;
using MediatR;
using Shared;

namespace BLL.Features.Products.Delete;
public sealed class DeleteProductCommand : IRequest<Response<string>>
{
    public int Id { get; set; }
}

public class DeleteProductCommandHandler(IProductRepository ProductRepository) :
    IRequestHandler<DeleteProductCommand, Response<string>>
{

    public async Task<Response<string>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await ProductRepository.GetByIdAsync(command.Id, cancellationToken);
        if (product == null)
        {
            return new Response<string>(ResponseMessage.ProductNotFound, false);
        }
        await ProductRepository.DeleteAsync(product, cancellationToken);
        return new Response<string>(ResponseMessage.ProductDeleted, true);
    }
}
