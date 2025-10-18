using AutoMapper;
using BLL.Abstractions;
using BLL.Features.Products.Add;
using Domain.Entities;
using Moq;
using Shared;
using Shouldly;

namespace Catalog.UnitTest;
public class AddProductCommandHandlerTest
{
    private readonly Mock<IProductRepository> _productRepoMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly AddProductCommandHandler _serviceToTest;

    public AddProductCommandHandlerTest()
    {

        _serviceToTest = new AddProductCommandHandler(_productRepoMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task AddProduct_ValidPayload_ShouldReturnSuccessful()
    {
        // Arrange
        var command = new AddProductCommand
        {
            Amount = 10,
            CategoryId = 1,
            Description = "Test product description",
            Name = "Test Product",
            Price = 99.99M,
            Image = "test-image-url"

        };

        var product = new Product
        {
            Amount = 10,
            CategoryId = 1,
            Description = "Test product description",
            Name = "Test Product",
            Price = 99.99M,
            Image = "test-image-url"
        };

        var cancellationToken = CancellationToken.None;

        _productRepoMock.Setup(x => x.CreateAsync(product, cancellationToken));
        _mapperMock.Setup(x => x.Map<Product>(command)).Returns(product);

        // Act
        var response = await _serviceToTest.Handle(command, cancellationToken);

        // Assert
        response.Message.ShouldBe(ResponseMessage.ProductAdded);
        response.Succeeded.ShouldBeTrue();

    }

}
