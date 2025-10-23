using CartService.DAL.Database.Repository;
using DAL.Models;
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
            Id = 1,
            Name = "Test Cart",
            Price = 10.0M,
            Quantity = 2,
            Image = "test-image-url"
        };

        var repo = new CartRepository(mockDatabase.Object);

        // Act
        await repo.AddItemAsync(cart, CancellationToken.None);

        // Assert
        mockCollection.Verify(c => c.InsertOneAsync(cart, null, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task AddItemAsync_InvalidCart_ShouldThrowArgumentException()
    {
        // Arrange
        var mockCollection = new Mock<IMongoCollection<Cart>>();
        var mockDatabase = new Mock<IMongoDatabase>();
        mockDatabase.Setup(db => db.GetCollection<Cart>(It.IsAny<string>(), null))
            .Returns(mockCollection.Object);

        var cart = new Cart
        {
            Id = 0,
            Name = "",
            Price = 0M,
            Quantity = 2,
            Image = "test-image-url"
        };

        var repo = new CartRepository(mockDatabase.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await repo.AddItemAsync(cart, CancellationToken.None));
        mockCollection.Verify(c => c.InsertOneAsync(It.IsAny<Cart>(), null, It.IsAny<CancellationToken>()), Times.Never);
    }
}