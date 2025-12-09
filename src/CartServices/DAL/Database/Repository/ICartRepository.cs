
using DAL;
using DAL.Entities;

namespace CartServices.DAL.Database.Repository;

public interface ICartRepository
{
	Task<bool> AddItemAsync (AddItemToCartRequest cart, CancellationToken cancellationToken);
	Task<Cart> GetCartItemsAsync (string cartKey, CancellationToken cancellationToken);
	Task<bool> RemoveItemAsync (string cartKey, int itemId, CancellationToken cancellationToken);
	Task UpdateCartItemsFromMessageConsumerAsync (int id, decimal price, string name, CancellationToken cancellationToken);
}