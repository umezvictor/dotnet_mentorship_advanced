using CartService.BLL.Abstractions;
using CartService.Domain;
using MongoDB.Driver;

namespace CartService.DAL.Database.Repository;
public sealed class CartRepository : ICartRepository
{
    private const string CollectionName = "Cart";
    private readonly IMongoCollection<Cart> _collection;

    public CartRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Cart>(CollectionName);
    }

    public async Task AddItemAsync(Cart cart)
    {

        await _collection.InsertOneAsync(cart);
    }

    public async Task<List<Cart>> GetItemsAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }


    public async Task<bool> RemoveItemAsync(int id)
    {
        var result = await _collection.DeleteOneAsync(c => c.Id == id);
        return result.DeletedCount > 0;
    }
}