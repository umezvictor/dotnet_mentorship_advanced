using AutoMapper;
using BLL.Abstractions;
using BLL.Services;
using DAL.Database;
using DAL.Database.Repository;
using DAL.Entities;
using Moq;
using Shared.Dto;
using Shared.ResponseObjects;
using Shouldly;

namespace BLL.UnitTest;
public class AddProductTest
{
	private readonly Mock<IProductRepository> _productRepoMock = new();
	private readonly Mock<IApplicationDbContext> _dbContextMock = new();
	private readonly Mock<IMapper> _mapperMock = new();
	private readonly Mock<ILinkService> _linkServiceMock = new();
	private readonly ProductService _serviceToTest;

	public AddProductTest ()
	{

		_serviceToTest = new ProductService( _productRepoMock.Object, _mapperMock.Object, _linkServiceMock.Object, _dbContextMock.Object );
	}

	[Fact]
	public async Task AddProduct_ValidPayload_ShouldReturnProductAdded ()
	{
		// Arrange
		var request = new AddProductRequest
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

		_productRepoMock.Setup( x => x.CreateAsync( product, cancellationToken ) );
		_mapperMock.Setup( x => x.Map<Product>( request ) ).Returns( product );

		// Act
		var response = await _serviceToTest.AddProductAsync( request, cancellationToken );

		// Assert
		response.Message.ShouldBe( ResponseMessage.ProductAdded );
		response.Succeeded.ShouldBeTrue();

	}

}
