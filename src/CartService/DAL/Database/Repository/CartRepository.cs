using DAL.Models;
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

    public async Task AddItemAsync(Cart cart, CancellationToken cancellationToken)
    {
        if (!cart.IsValid())
        {
            throw new ArgumentException("Invalid cart item");
        }
        await _collection.InsertOneAsync(cart, options: null, cancellationToken);
    }

    public async Task<List<Cart>> GetItemsAsync(CancellationToken cancellationToken)
    {
        return await _collection.Find(_ => true).ToListAsync(cancellationToken);
    }


    public async Task<bool> RemoveItemAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _collection.DeleteOneAsync(c => c.Id == id, cancellationToken);
        return result.DeletedCount > 0;
    }
}