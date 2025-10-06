using CartService.BLL.Abstractions;
using MediatR;
using Shared;

namespace BLL.Features.Delete;
public sealed class DeleteItemFromCartCommand : IRequest<Response<string>>
{
    public int Id { get; set; }
}

public class DeleteItemFromCartCommandHandler(ICartRepository cartRepository) :
    IRequestHandler<DeleteItemFromCartCommand, Response<string>>
{

    public async Task<Response<string>> Handle(DeleteItemFromCartCommand command, CancellationToken cancellationToken)
    {
        if (await cartRepository.RemoveItemAsync(command.Id))
        {
            return new Response<string>(ResponseMessage.ItemRemovedFromCart, true);
        }
        return new Response<string>(ResponseMessage.ItemNotRemoved, false);
    }
}



