using DAL.Entities;
using MongoDB.Driver;

namespace CartServices.DAL.Database.Repository;
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

        await _collection.InsertOneAsync(cart, options: null, cancellationToken);
    }

    public async Task<Cart> GetCartItemsAsync(string cartKey, CancellationToken cancellationToken)
    {
        return await _collection.Find(c => c.CartKey == cartKey).FirstOrDefaultAsync(cancellationToken);
    }


    public async Task<bool> RemoveItemAsync(string cartKey, int itemId, CancellationToken cancellationToken)
    {
        var cart = await _collection.Find(c => c.CartKey == cartKey).FirstOrDefaultAsync(cancellationToken);
        if (cart is null)
            return false;


        var itemToRemove = cart.CartItems.FirstOrDefault(i => i.Id == itemId);
        if (itemToRemove is null)
            return false;

        cart.CartItems.Remove(itemToRemove);

        var update = Builders<Cart>.Update.Set(c => c.CartItems, cart.CartItems);
        var result = await _collection.UpdateOneAsync(c => c.CartKey == cartKey, update, cancellationToken: cancellationToken);
        return result.ModifiedCount > 0;


    }
}