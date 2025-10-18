using AutoMapper;
using BLL.Features.Add;
using CartService.BLL.Abstractions;
using Moq;
using Shared;
using Shouldly;

namespace Cart.UnitTest.TestCases;
public class AddItemToCartHandlerTest
{
    private readonly Mock<ICartRepository> _cartRepoMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly AddToCartCommandHandler _serviceToTest;

    public AddItemToCartHandlerTest()
    {

        _serviceToTest = new AddToCartCommandHandler(_cartRepoMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task AddItemToCart_WhenGivenValidPayload_ShouldSaveItemAndReturnSuccessMessage()
    {
        // Arrange
        var command = new AddToCartCommand
        {
            Id = 1,
            Image = "image_url",
            Name = "Test Item",
            Price = 10.5m,
            Quantity = 2
        };

        var cartItem = new CartService.Domain.Cart
        {
            Id = 1,
            Image = "image_url",
            Name = "Test Item",
            Price = 10.5m,
            Quantity = 2
        };

        var cancellationToken = CancellationToken.None;

        _cartRepoMock.Setup(x => x.AddItemAsync(cartItem));
        _mapperMock.Setup(x => x.Map<CartService.Domain.Cart>(command)).Returns(cartItem);

        // Act
        var response = await _serviceToTest.Handle(command, cancellationToken);

        // Assert
        response.Message.ShouldBe(ResponseMessage.ItemAddedToCart);
        response.Succeeded.ShouldBeTrue();

    }

}
