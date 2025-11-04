using BLL.Dtos;
using DAL.Entities;

namespace BLL.Services;
public interface ICartService
{
    Task<bool> AddItemToCartAsync(Cart cart, CancellationToken cancellationToken);
    Task<bool> DeleteCartItemAsync(DeleteItemFromCartRequest request, CancellationToken cancellationToken);
    Task<Cart?> GetCartItemsAsync(string cartKey, CancellationToken cancellationToken);
}