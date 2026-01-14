using AutoMapper;
using BLL.Abstractions;
using BLL.Services;
using DAL.Database;
using DAL.Database.Repository;
using DAL.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using RabbitMQ;
using Shared.Dto;
using Shared.ResponseObjects;
using Shouldly;

namespace BLL.UnitTest;
public class DeleteProductTest
{
	private readonly Mock<IProductRepository> _productRepoMock = new();
	private readonly Mock<IApplicationDbContext> _dbContextMock = new();
	private readonly Mock<ILinkService> _linkServiceMock = new();
	private readonly Mock<IMapper> _mapperMock = new();
	private readonly Mock<IRabbitMqClient> _rabbitMqClientMock = new();
	private readonly Mock<ILogger<ProductService>> _loggerMock = new();

	private readonly ProductService _serviceToTest;

	public DeleteProductTest()
	{
		_serviceToTest = new ProductService(_productRepoMock.Object, _mapperMock.Object, _linkServiceMock.Object, _dbContextMock.Object, _rabbitMqClientMock.Object,
			_loggerMock.Object);
	}

	[Fact]
	public async Task DeleteProduct_WhenGivenValidId_ShouldDeleteProductAndReturnSuccessMessage()
	{
		var request = new DeleteProductRequest
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
			Image = "test-image-url",
			Category = new Category { Id = 1, Name = "Test Category", Image = "category-image-url" }

		};
		var cancellationToken = CancellationToken.None;

		_productRepoMock.Setup(x => x.GetByIdAsync(request.Id, cancellationToken)).ReturnsAsync(product);
		_productRepoMock.Setup(x => x.DeleteAsync(product, cancellationToken));

		// Act
		var response = await _serviceToTest.DeleteProductAsync(request, cancellationToken);

		// Assert
		response.Message.ShouldBe(ResponseMessage.ProductDeleted);
		response.Succeeded.ShouldBeTrue();

	}


	[Fact]
	public async Task DeleteProductWhenGivenInValidId_ShouldReturnProductNotFound()
	{
		var request = new DeleteProductRequest
		{
			Id = 1
		};

		Product? product = null;

		var cancellationToken = CancellationToken.None;

		_productRepoMock.Setup(x => x.GetByIdAsync(request.Id, cancellationToken)).ReturnsAsync(product);

		// Act
		var response = await _serviceToTest.DeleteProductAsync(request, cancellationToken);

		// Assert
		response.Message.ShouldBe(ResponseMessage.ProductNotFound);
		response.Succeeded.ShouldBeFalse();

	}
}

