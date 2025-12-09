using DAL.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.IntegrationTests;
public static class TestData
{

	public static Cart CartData ()
	{
		return new Cart
		{
			CartKey = "sample-cart-123",
			CartItems = new List<CartItem>
		 {
		new CartItem
		{
			Id = 1,
			Name = "Wireless Mouse",
			Image = "mouse.jpg",
			Price = 25.99m,
			Quantity = 2
		},
		new CartItem
		{
			Id = 2,
			Name = "Mechanical Keyboard",
			Image = "keyboard.jpg",
			Price = 79.50m,
			Quantity = 1
		},
		new CartItem
		{
			Id = 3,
			Name = "USB-C Hub",
			Image = "hub.jpg",
			Price = 34.99m,
			Quantity = 3
		}
	}
		};
	}
}




public class TestDocument
{
	[BsonId]
	[BsonRepresentation( BsonType.ObjectId )]
	public string CartKey { get; set; }

	public List<CartItem> CartItems { get; set; }

	public static TestDocument DummyData1 ()
	{
		return new TestDocument
		{
			CartKey = "sample-cart-123",
			CartItems = new List<CartItem>
			{
				new CartItem
				{
					Id = 1,
					Name = "Wireless Mouse",
					Image = "mouse.jpg",
					Price = 25.99m,
					Quantity = 2
				},
				new CartItem
				{
					Id = 2,
					Name = "Mechanical Keyboard",
					Image = "keyboard.jpg",
					Price = 79.50m,
					Quantity = 1
				},
				new CartItem
				{
					Id = 3,
					Name = "USB-C Hub",
					Image = "hub.jpg",
					Price = 34.99m,
					Quantity = 3
				}
			}
		};
	}




}