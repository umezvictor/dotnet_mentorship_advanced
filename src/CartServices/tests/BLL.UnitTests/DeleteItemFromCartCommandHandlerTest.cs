using BLL.Dtos;
using BLL.Services;
using CartServices.DAL.Database.Repository;
using Moq;
using Shouldly;

namespace BLL.UnitTests;

public class DeleteItemFromCartCommandHandlerTest
{
	private readonly Mock<ICartRepository> _cartRepoMock = new();
	private readonly CartService _serviceToTest;

	public DeleteItemFromCartCommandHandlerTest()
	{

		_serviceToTest = new CartService(_cartRepoMock.Object);
	}

	[Fact]
	public async Task RemoveItemFromCart_WhenGivenValidId_ShouldDeleteItemAndReturnSuccessMessage()
	{
		var request = new DeleteItemFromCartRequest
		{
			Id = 1,
			CartKey = "test-cart-key"
		};
		var cancellationToken = CancellationToken.None;

		_cartRepoMock.Setup(x => x.RemoveItemAsync(request.CartKey, request.Id, cancellationToken)).ReturnsAsync(true);

		// Act
		var response = await _serviceToTest.DeleteCartItemAsync(request, cancellationToken);

		// Assert
		response.ShouldBeTrue();

	}


	[Fact]
	public async Task RemoveItemFromCart_WhenGivenInValidId_ShouldReturnItemNotRemoved()
	{
		var request = new DeleteItemFromCartRequest
		{
			Id = 1,
			CartKey = "test-cart-key"
		};
		var cancellationToken = CancellationToken.None;

		_cartRepoMock.Setup(x => x.RemoveItemAsync(request.CartKey, request.Id, cancellationToken)).ReturnsAsync(false);

		// Act
		var response = await _serviceToTest.DeleteCartItemAsync(request, cancellationToken);

		// Assert
		response.ShouldBeFalse();

	}
}

