using BLL.Features.Delete;
using CartService.BLL.Abstractions;
using Moq;
using Shared;
using Shouldly;

namespace TestSuite.UnitTest;


public class DeleteItemFromCartCommandHandlerTest
{
    private readonly Mock<ICartRepository> _cartRepoMock = new();
    private readonly DeleteItemFromCartCommandHandler _serviceToTest;

    public DeleteItemFromCartCommandHandlerTest()
    {

        _serviceToTest = new DeleteItemFromCartCommandHandler(_cartRepoMock.Object);
    }

    [Fact]
    public async Task RemoveItemFromCart_WhenGivenValidId_ShouldDeleteItemAndReturnSuccessMessage()
    {
        var command = new DeleteItemFromCartCommand
        {
            Id = 1
        };
        var cancellationToken = CancellationToken.None;

        _cartRepoMock.Setup(x => x.RemoveItemAsync(command.Id)).ReturnsAsync(true);

        // Act
        var response = await _serviceToTest.Handle(command, cancellationToken);

        // Assert
        response.Message.ShouldBe(ResponseMessage.ItemRemovedFromCart);
        response.Succeeded.ShouldBeTrue();

    }


    [Fact]
    public async Task RemoveItemFromCart_WhenGivenInValidId_ShouldReturnItemNotRemoved()
    {
        var command = new DeleteItemFromCartCommand
        {
            Id = 1
        };
        var cancellationToken = CancellationToken.None;

        _cartRepoMock.Setup(x => x.RemoveItemAsync(command.Id)).ReturnsAsync(false);

        // Act
        var response = await _serviceToTest.Handle(command, cancellationToken);

        // Assert
        response.Message.ShouldBe(ResponseMessage.ItemNotRemoved);
        response.Succeeded.ShouldBeFalse();

    }
}

