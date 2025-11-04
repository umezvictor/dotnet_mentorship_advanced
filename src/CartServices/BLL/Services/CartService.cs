using BLL.Dtos;
using CartServices.DAL.Database.Repository;
using DAL.Entities;

namespace BLL.Services;
public sealed class CartService(ICartRepository cartRepository) : ICartService
{
    public async Task<bool> AddItemToCartAsync(Cart cart, CancellationToken cancellationToken)
    {
        await cartRepository.AddItemAsync(cart, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteCartItemAsync(DeleteItemFromCartRequest request, CancellationToken cancellationToken)
    {
        if (await cartRepository.RemoveItemAsync(request.CartKey, request.Id, cancellationToken))
            return true;
        return false;
    }


    public async Task<Cart?> GetCartItemsAsync(string cartKey, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetCartItemsAsync(cartKey, cancellationToken);
        if (cart != null)
            return cart;
        return null;
    }
}
