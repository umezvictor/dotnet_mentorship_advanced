using DAL.Database;
using DAL.Database.Repository;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.UnitTest;

public class ProductRepositoryTests
{
	private ApplicationDbContext GetDbContext ()
	{
		var options = new DbContextOptionsBuilder<ApplicationDbContext>()
			.UseInMemoryDatabase( databaseName: Guid.NewGuid().ToString() )
			.Options;
		return new ApplicationDbContext( options );
	}

	private Product GetValidProduct (int categoryId = 1)
	{
		return new Product
		{
			Name = "Test Product",
			Description = "Test Description",
			Price = 100.0M,
			Amount = 10,
			Image = "test-image-url",
			CategoryId = categoryId,
			Category = new Category { Id = categoryId, Name = "Test Category" }
		};
	}

	[Fact]
	public async Task CreateAsync_ShouldAddProductAndReturnId_WhenProductIsValid ()
	{
		// Arrange
		var dbContext = GetDbContext();
		var repository = new ProductRepository( dbContext );
		var product = GetValidProduct();

		// Act
		var id = await repository.CreateAsync( product, CancellationToken.None );

		// Assert
		Assert.True( id > 0 );
		var saved = await dbContext.Products.FindAsync( id );
		Assert.NotNull( saved );
		Assert.Equal( "Test Product", saved.Name );
	}


	[Fact]
	public async Task GetByIdAsync_ShouldReturnProduct_WhenExists ()
	{
		// Arrange
		var dbContext = GetDbContext();
		var product = GetValidProduct();
		dbContext.Products.Add( product );
		await dbContext.SaveChangesAsync();
		var repository = new ProductRepository( dbContext );

		// Act
		var result = await repository.GetByIdAsync( product.Id, CancellationToken.None );

		// Assert
		Assert.NotNull( result );
		Assert.Equal( product.Name, result.Name );
	}

	[Fact]
	public async Task GetAllProductsAsync_ShouldReturnAllProducts ()
	{
		// Arrange
		var dbContext = GetDbContext();
		dbContext.Products.AddRange(
			GetValidProduct( 1 ),
			GetValidProduct( 2 )
		);
		await dbContext.SaveChangesAsync();
		var repository = new ProductRepository( dbContext );

		// Act
		var result = await repository.GetAllProductsAsync( CancellationToken.None );

		// Assert
		Assert.Equal( 2, result.Count );
	}

	[Fact]
	public async Task DeleteAsync_ShouldRemoveProduct ()
	{
		// Arrange
		var dbContext = GetDbContext();
		var product = GetValidProduct();
		dbContext.Products.Add( product );
		await dbContext.SaveChangesAsync();
		var repository = new ProductRepository( dbContext );

		// Act
		await repository.DeleteAsync( product, CancellationToken.None );

		// Assert
		var deleted = await dbContext.Products.FindAsync( product.Id );
		Assert.Null( deleted );
	}

	[Fact]
	public async Task UpdateAsync_ShouldUpdateProduct ()
	{
		// Arrange
		var dbContext = GetDbContext();
		var product = GetValidProduct();
		dbContext.Products.Add( product );
		await dbContext.SaveChangesAsync();
		var repository = new ProductRepository( dbContext );

		// Act
		product.Name = "Updated Name";
		var updated = await repository.UpdateAsync( product, CancellationToken.None );

		// Assert
		Assert.Equal( "Updated Name", updated.Name );
		var fetchedProduct = await dbContext.Products.FindAsync( product.Id );
		Assert.Equal( "Updated Name", fetchedProduct!.Name );
	}
}