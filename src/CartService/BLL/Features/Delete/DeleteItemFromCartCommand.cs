using CartService.DAL.Database.Repository;
using MediatR;

namespace BLL.Features.Delete;
public sealed class DeleteItemFromCartCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteItemFromCartCommandHandler(ICartRepository cartRepository) :
    IRequestHandler<DeleteItemFromCartCommand, bool>
{

    public async Task<bool> Handle(DeleteItemFromCartCommand command, CancellationToken cancellationToken)
    {
        if (await cartRepository.RemoveItemAsync(command.Id, cancellationToken))
            return true;
        return false;
    }
}



