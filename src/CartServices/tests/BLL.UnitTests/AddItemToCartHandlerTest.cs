using BLL.Services;
using CartServices.DAL.Database.Repository;
using DAL.Entities;
using Moq;
using Shouldly;

namespace BLL.UnitTests;
public class AddItemToCartHandlerTest
{
    private readonly Mock<ICartRepository> _cartRepoMock = new();
    private readonly CartService _serviceToTest;

    public AddItemToCartHandlerTest()
    {

        _serviceToTest = new CartService(_cartRepoMock.Object);
    }

    [Fact]
    public async Task AddItemToCart_WhenGivenValidPayload_ShouldSaveItemAndReturnSuccessMessage()
    {
        // Arrange
        var cartData = new Cart
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



        var cancellationToken = CancellationToken.None;

        _cartRepoMock.Setup(x => x.AddItemAsync(cartData, cancellationToken));

        // Act
        var response = await _serviceToTest.AddItemToCartAsync(cartData, cancellationToken);

        response.ShouldBeTrue();

    }

}
