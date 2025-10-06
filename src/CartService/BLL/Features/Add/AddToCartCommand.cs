using AutoMapper;
using CartService.BLL.Abstractions;
using CartService.Domain;
using MediatR;
using Shared;

namespace BLL.Features.Add;
public sealed class AddToCartCommand : IRequest<Response<string>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class AddToCartCommandHandler(ICartRepository cartRepository, IMapper mapper) :
    IRequestHandler<AddToCartCommand, Response<string>>
{

    public async Task<Response<string>> Handle(AddToCartCommand command, CancellationToken cancellationToken)
    {
        await cartRepository.AddItemAsync(mapper.Map<Cart>(command));
        return new Response<string>(ResponseMessage.ItemAddedToCart, true);
    }
}



