using BLL.Dtos;
using DAL;
using DAL.Entities;

namespace BLL.Services;
public interface ICartService
{
    Task<bool> AddItemToCartAsync(AddItemToCartRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteCartItemAsync(DeleteItemFromCartRequest request, CancellationToken cancellationToken);
    Task<Cart?> GetCartItemsAsync(string cartKey, CancellationToken cancellationToken);
}