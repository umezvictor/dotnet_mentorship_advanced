
using CartService.Domain;

namespace CartService.BLL.Abstractions;
public interface ICartRepository
{
    Task AddItemAsync(Cart cart);
    Task<List<Cart>> GetItemsAsync();
    Task<bool> RemoveItemAsync(int id);
}
