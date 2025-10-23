
using DAL.Models;

namespace CartService.DAL.Database.Repository;

public interface ICartRepository
{
    Task AddItemAsync(Cart cart, CancellationToken cancellationToken);
    Task<List<Cart>> GetItemsAsync(CancellationToken cancellationToken);
    Task<bool> RemoveItemAsync(int id, CancellationToken cancellationToken);
}