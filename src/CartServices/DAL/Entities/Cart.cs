using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Entities;
public class Cart
{
	[BsonId]
	public string CartKey { get; set; }

	public List<CartItem> CartItems { get; set; } = [];

}


