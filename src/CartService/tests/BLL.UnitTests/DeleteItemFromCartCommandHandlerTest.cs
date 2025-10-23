using BLL.Features.Delete;
using CartService.DAL.Database.Repository;
using Moq;
using Shouldly;

namespace BLL.UnitTests;


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

        _cartRepoMock.Setup(x => x.RemoveItemAsync(command.Id, cancellationToken)).ReturnsAsync(true);

        // Act
        var response = await _serviceToTest.Handle(command, cancellationToken);

        // Assert
        response.ShouldBeTrue();

    }


    [Fact]
    public async Task RemoveItemFromCart_WhenGivenInValidId_ShouldReturnItemNotRemoved()
    {
        var command = new DeleteItemFromCartCommand
        {
            Id = 1
        };
        var cancellationToken = CancellationToken.None;

        _cartRepoMock.Setup(x => x.RemoveItemAsync(command.Id, cancellationToken)).ReturnsAsync(false);

        // Act
        var response = await _serviceToTest.Handle(command, cancellationToken);

        // Assert
        response.ShouldBeFalse();

    }
}

