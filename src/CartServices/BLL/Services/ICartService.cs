using BLL.Dtos;
using DAL;
using DAL.Entities;
using Shared.RabbitMQ;

namespace BLL.Services;
public interface ICartService
{
    Task<bool> AddItemToCartAsync(AddItemToCartRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteCartItemAsync(DeleteItemFromCartRequest request, CancellationToken cancellationToken);
    Task<Cart?> GetCartItemsAsync(string cartKey, CancellationToken cancellationToken);
    Task UpdateCartItemsFromMessageConsumerAsync(ProductUpdatedContract request, CancellationToken cancellationToken);
}