using DAL.Database;
using DAL.Database.Repository;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.UnitTest;

public class CategoryRepositoryTests
{
	private ApplicationDbContext GetDbContext ()
	{
		var options = new DbContextOptionsBuilder<ApplicationDbContext>()
			.UseInMemoryDatabase( databaseName: "TestDb" )
			.Options;
		return new ApplicationDbContext( options );
	}

	[Fact]
	public async Task CreateAsync_ShouldAddCategoryAndReturnId ()
	{
		// Arrange
		var dbContext = GetDbContext();
		var repository = new CategoryRepository( dbContext );
		var category = new Category { Name = "Test Category", Image = "test.png" };

		// Act
		var id = await repository.CreateAsync( category, CancellationToken.None );

		// Assert
		Assert.True( id > 0 );
		var saved = await dbContext.Category.FindAsync( id );
		Assert.NotNull( saved );
		Assert.Equal( "Test Category", saved.Name );
	}

	[Fact]
	public async Task GetByIdAsync_ShouldReturnCategory_WhenExists ()
	{
		// Arrange
		var dbContext = GetDbContext();
		var category = new Category { Name = "Test Category", Image = "test.png" };
		dbContext.Category.Add( category );
		await dbContext.SaveChangesAsync();
		var repository = new CategoryRepository( dbContext );

		// Act
		var result = await repository.GetByIdAsync( category.Id, CancellationToken.None );

		// Assert
		Assert.NotNull( result );
		Assert.Equal( category.Name, result.Name );
	}

	[Fact]
	public async Task GetAllCategoriesAsync_ShouldReturnAllCategories ()
	{
		// Arrange
		var dbContext = GetDbContext();
		dbContext.Category.AddRange(
			new Category { Name = "Cat1", Image = "img1.png" },
			new Category { Name = "Cat2", Image = "img2.png" }
		);
		await dbContext.SaveChangesAsync();
		var repository = new CategoryRepository( dbContext );

		// Act
		var result = await repository.GetAllCategoriesAsync( CancellationToken.None );

		// Assert
		Assert.Equal( 2, result.Count );
	}



	[Fact]
	public async Task UpdateAsync_ShouldUpdateCategory ()
	{
		// Arrange
		var dbContext = GetDbContext();
		var category = new Category { Name = "OldName", Image = "old.png" };
		dbContext.Category.Add( category );
		await dbContext.SaveChangesAsync();
		var repository = new CategoryRepository( dbContext );

		// Act
		category.Name = "NewName";
		var updated = await repository.UpdateAsync( category, CancellationToken.None );

		// Assert
		Assert.Equal( "NewName", updated.Name );
		var fetched = await dbContext.Category.FindAsync( category.Id );
		Assert.Equal( "NewName", fetched.Name );
	}
}