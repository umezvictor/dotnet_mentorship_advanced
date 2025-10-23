using AutoMapper;
using CartService.DAL.Database.Repository;
using DAL.Models;
using MediatR;

namespace BLL.Features.Add;
public sealed class AddToCartCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class AddToCartCommandHandler(ICartRepository cartRepository, IMapper mapper) :
    IRequestHandler<AddToCartCommand, bool>
{

    public async Task<bool> Handle(AddToCartCommand command, CancellationToken cancellationToken)
    {
        await cartRepository.AddItemAsync(mapper.Map<Cart>(command), cancellationToken);
        return true;
    }
}



