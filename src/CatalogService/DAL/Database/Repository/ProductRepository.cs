using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;
using Shared.ResponseObjects;

namespace DAL.Database.Repository;
public sealed class ProductRepository (ApplicationDbContext _context) : IProductRepository
{

	public async Task<int> CreateAsync (Product product, CancellationToken cancellationToken)
	{

		var createdItem = await _context.Products.AddAsync( product, cancellationToken );
		await _context.SaveChangesAsync( cancellationToken );

		return createdItem.Entity.Id;
	}

	public async Task<Product?> GetByIdAsync (long id, CancellationToken cancellationToken)
	{
		return await _context.Products
			.Where( x => x.Id == id )
			.FirstOrDefaultAsync( cancellationToken );
	}

	public async Task<List<Product>> GetAllProductsAsync (CancellationToken cancellationToken)
	{
		return await _context.Products
			.AsNoTracking()
			.ToListAsync( cancellationToken );
	}


	public async Task<PaginatedResponse<List<ProductDto>>> GetProductsByCategoryIdAsync (int categoryId, int pageNumber, int pageSize, CancellationToken cancellationToken)
	{
		PaginatedResponse<List<ProductDto>> paginatedResponse = new PaginatedResponse<List<ProductDto>>();

		if (pageNumber <= 0) pageNumber = 1;
		if (pageSize <= 0) pageSize = 10;

		paginatedResponse.PageSize = pageSize;
		var query = _context.Products.Where( u => u.CategoryId == categoryId );
		paginatedResponse.TotalCount = await query.CountAsync();

		paginatedResponse.Data = await _context.Products.Where( p => p.CategoryId == categoryId )
			.Select( o => new ProductDto
			{
				Id = o.Id,
				Amount = o.Amount,
				Image = o.Image ?? "",
				Name = o.Name,
				Price = o.Price,
				CategoryId = o.CategoryId,

			} )
		.Skip( (pageNumber - 1) * pageSize )
		.Take( pageSize )
		.ToListAsync();
		return paginatedResponse;
	}


	public async Task DeleteAsync (Product product, CancellationToken cancellationToken)
	{
		_context.Products.Remove( product );
		await _context.SaveChangesAsync( cancellationToken );
	}

	public async Task<Product> UpdateAsync (Product product, CancellationToken cancellationToken)
	{
		var updatedItem = _context.Products.Update( product );
		await _context.SaveChangesAsync( cancellationToken );
		return updatedItem.Entity;
	}
}