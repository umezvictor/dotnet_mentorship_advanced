using CartServices.DAL.Database.Repository;
using DAL.Entities;
using MongoDB.Driver;
using Moq;

namespace DAL.UnitTest;
public class CartRepositoryTests
{
    [Fact]
    public async Task AddItemAsync_ValidCart_ShouldInsertItem()
    {
        // Arrange
        var mockCollection = new Mock<IMongoCollection<Cart>>();
        var mockDatabase = new Mock<IMongoDatabase>();
        mockDatabase.Setup(db => db.GetCollection<Cart>(It.IsAny<string>(), null))
            .Returns(mockCollection.Object);

        var cart = new Cart
        {
            CartKey = "1",
            CartItems = new List<CartItem>
        {
            new CartItem
            {
                Id = 1,
                Name = "Test Item",
                Price = 10.5M,
                Quantity = 2,
                Image = "test-image-url"
            }
        }
        };

        var repo = new CartRepository(mockDatabase.Object);

        // Act
        await repo.AddItemAsync(cart, CancellationToken.None);

        // Assert
        mockCollection.Verify(c => c.InsertOneAsync(cart, null, CancellationToken.None), Times.Once);
    }


}