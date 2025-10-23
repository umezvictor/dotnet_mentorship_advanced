using BLL.Abstractions;
using BLL.Features.Products.Delete;
using Domain.Entities;
using Moq;
using Shared;
using Shouldly;

namespace BLL.UnitTest;


public class DeleteProductCommandHandlerTest
{
    private readonly Mock<IProductRepository> _productRepoMock = new();
    private readonly DeleteProductCommandHandler _serviceToTest;

    public DeleteProductCommandHandlerTest()
    {

        _serviceToTest = new DeleteProductCommandHandler(_productRepoMock.Object);
    }

    [Fact]
    public async Task RemoveItemFromCart_WhenGivenValidId_ShouldDeleteItemAndReturnSuccessMessage()
    {
        var command = new DeleteProductCommand
        {
            Id = 1
        };

        var product = new Product
        {
            Id = 1,
            Amount = 10,
            CategoryId = 1,
            Description = "Test product description",
            Name = "Test Product",
            Price = 99.99M,
            Image = "test-image-url"
        };
        var cancellationToken = CancellationToken.None;

        _productRepoMock.Setup(x => x.GetByIdAsync(command.Id, cancellationToken)).ReturnsAsync(product);
        _productRepoMock.Setup(x => x.DeleteAsync(product, cancellationToken));

        // Act
        var response = await _serviceToTest.Handle(command, cancellationToken);

        // Assert
        response.Message.ShouldBe(ResponseMessage.ProductDeleted);
        response.Succeeded.ShouldBeTrue();

    }


    [Fact]
    public async Task RemoveItemFromCart_WhenGivenInValidId_ShouldReturnItemNotRemoved()
    {
        var command = new DeleteProductCommand
        {
            Id = 1
        };

        Product product = null;

        var cancellationToken = CancellationToken.None;

        _productRepoMock.Setup(x => x.GetByIdAsync(command.Id, cancellationToken)).ReturnsAsync(product);

        // Act
        var response = await _serviceToTest.Handle(command, cancellationToken);

        // Assert
        response.Message.ShouldBe(ResponseMessage.ProductNotFound);
        response.Succeeded.ShouldBeFalse();

    }
}

