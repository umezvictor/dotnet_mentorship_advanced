using AutoMapper;
using BLL.Abstractions;
using BLL.Services;
using DAL.Database;
using DAL.Database.Repository;
using DAL.Entities;
using Moq;
using Shared.Dto;
using Shared.ResponseObjects;

namespace BLL.UnitTest;
public class GetProductByIdTest
{
	private readonly Mock<IProductRepository> _productRepoMock = new();
	private readonly Mock<IMapper> _mapperMock = new();
	private readonly Mock<ILinkService> _linkServiceMock = new();
	private readonly Mock<IApplicationDbContext> _dbContextMock = new();

	private readonly ProductService _serviceToTest;

	public GetProductByIdTest ()
	{

		_serviceToTest = new ProductService( _productRepoMock.Object, _mapperMock.Object, _linkServiceMock.Object, _dbContextMock.Object );
	}

	[Fact]
	public async Task GetProductById_ProductExists_ReturnsSuccessResponseWithLinks ()
	{
		// Arrange
		int id = 10;
		var product = new Product
		{
			Id = id,
			Name = "Test Product",
			Description = "Desc",
			Price = 15.5m,
			Amount = 3,
			CategoryId = 2,
			Image = "img"
		};

		var dto = new ProductDto
		{
			Id = id,
			Name = product.Name,
			Description = product.Description,
			Price = product.Price,
			Amount = product.Amount,
			CategoryId = product.CategoryId,
			Image = product.Image,
			Links = new List<Link>()
		};

		_productRepoMock
			.Setup( r => r.GetByIdAsync( id, It.IsAny<CancellationToken>() ) )
			.ReturnsAsync( product );

		_mapperMock
			.Setup( m => m.Map<ProductDto>( product ) )
			.Returns( dto );

		_linkServiceMock
			.Setup( l => l.GenerateLinks( "GetProduct", It.IsAny<object>(), "self", "GET" ) )
			.Returns( new Link( "getHref", "self", "GET" ) );

		_linkServiceMock
			.Setup( l => l.GenerateLinks( "DeleteProduct", It.IsAny<object>(), "delete-product", "DELETE" ) )
			.Returns( new Link( "delHref", "delete-product", "DELETE" ) );

		_linkServiceMock
			.Setup( l => l.GenerateLinks( "UpdateProduct", It.IsAny<object>(), "update-product", "PUT" ) )
			.Returns( new Link( "updHref", "update-product", "PUT" ) );



		// Act
		var response = await _serviceToTest.GetProductByIdAsync( id, CancellationToken.None );

		// Assert
		Assert.True( response.Succeeded );
		Assert.Equal( ResponseMessage.Success, response.Message );
		Assert.NotNull( response.Data );
		Assert.Equal( id, response.Data.Id );
		Assert.Equal( 3, response.Data.Links.Count );

		_productRepoMock.Verify( r => r.GetByIdAsync( id, It.IsAny<CancellationToken>() ), Times.Once );
		_mapperMock.Verify( m => m.Map<ProductDto>( product ), Times.Once );
		_linkServiceMock.Verify( l => l.GenerateLinks( "GetProduct", It.IsAny<object>(), "self", "GET" ), Times.Once );
		_linkServiceMock.Verify( l => l.GenerateLinks( "DeleteProduct", It.IsAny<object>(), "delete-product", "DELETE" ), Times.Once );
		_linkServiceMock.Verify( l => l.GenerateLinks( "UpdateProduct", It.IsAny<object>(), "update-product", "PUT" ), Times.Once );
	}


	[Fact]
	public async Task GetProductById_ProductDoesNotExist_ReturnsNotFoundResponse ()
	{
		// Arrange
		long id = 99;
		_productRepoMock
			.Setup( r => r.GetByIdAsync( id, It.IsAny<CancellationToken>() ) )
			.ReturnsAsync( (Product?)null );

		// Act
		var response = await _serviceToTest.GetProductByIdAsync( id, CancellationToken.None );

		// Assert
		Assert.False( response.Succeeded );
		Assert.Equal( ResponseMessage.ProductNotFound, response.Message );
		Assert.Null( response.Data );

		_productRepoMock.Verify( r => r.GetByIdAsync( id, It.IsAny<CancellationToken>() ), Times.Once );
		_mapperMock.Verify( m => m.Map<ProductDto>( It.IsAny<Product>() ), Times.Never );
		_linkServiceMock.Verify( l => l.GenerateLinks( It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>() ), Times.Never );
	}
}









