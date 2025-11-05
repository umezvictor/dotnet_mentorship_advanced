using DAL;
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

    public async Task<bool> AddItemAsync(AddItemToCartRequest cart, CancellationToken cancellationToken)
    {
        var existingCart = await _collection.Find(c => c.CartKey == cart.CartKey).FirstOrDefaultAsync(cancellationToken);
        if (existingCart != null)
        {
            existingCart.CartItems.Add(cart.CartItem);
            var update = Builders<Cart>.Update.Set(c => c.CartItems, existingCart.CartItems);
            var result = await _collection.UpdateOneAsync(c => c.CartKey == cart.CartKey, update, cancellationToken: cancellationToken);
            return result.ModifiedCount > 0;
        }
        else
        {
            var newCart = new Cart
            {
                CartKey = cart.CartKey,
                CartItems = new List<CartItem> { cart.CartItem }
            };
            await _collection.InsertOneAsync(newCart, options: null, cancellationToken);
            return true;
        }
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