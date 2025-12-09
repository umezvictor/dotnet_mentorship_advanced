using System.Text.Json;
using AutoMapper;
using BLL.Abstractions;
using DAL.Database;
using DAL.Database.Repository;
using DAL.Entities;
using Shared.Dto;
using Shared.RabbitMQ;
using Shared.ResponseObjects;

namespace BLL.Services;
public sealed class ProductService (IProductRepository productRepository, IMapper mapper, ILinkService linkService,
	IApplicationDbContext dbContext) : IProductService
{

	public async Task<Response<int>> AddProductAsync (AddProductRequest request, CancellationToken cancellationToken)
	{
		var createdProductId = await productRepository.CreateAsync( mapper.Map<Product>( request ), cancellationToken );

		return new Response<int>( createdProductId, ResponseMessage.ProductAdded );
	}

	public async Task<Response<string>> DeleteProductAsync (DeleteProductRequest request, CancellationToken cancellationToken)
	{
		var product = await productRepository.GetByIdAsync( request.Id, cancellationToken );
		if (product == null)
		{
			return new Response<string>( ResponseMessage.ProductNotFound, false );
		}
		await productRepository.DeleteAsync( product, cancellationToken );
		return new Response<string>( ResponseMessage.ProductDeleted );
	}


	public async Task<Response<PaginatedResponse<List<ProductDto>>>> GetProductsByCategoryIdAsync (GetProductsQuery query, CancellationToken cancellationToken)
	{
		var paginatedResponse = await productRepository.GetProductsByCategoryIdAsync
			( query.CategoryId, query.PageNumber, query.PageSize, cancellationToken );

		if (paginatedResponse is null && !paginatedResponse.Data.Any())
			return new Response<PaginatedResponse<List<ProductDto>>>( ResponseMessage.NotItemsPresent, false );

		return new Response<PaginatedResponse<List<ProductDto>>>( paginatedResponse, ResponseMessage.Success );
	}

	public async Task<Response<string>> UpdateProductAsync (UpdateProductRequest request, CancellationToken cancellationToken)
	{
		var product = await productRepository.GetByIdAsync( request.Id, cancellationToken );
		if (product is null)
		{
			return new Response<string>( ResponseMessage.ProductNotFound, false );
		}

		using var transaction = await dbContext.Database.BeginTransactionAsync();

		try
		{
			var updatedProduct = await productRepository.UpdateAsync( mapper.Map( request, product ), cancellationToken );
			if (updatedProduct != null)
			{
				//save update product details to outbox table
				await dbContext.Outbox.AddAsync( new Outbox
				{

					CreatedOnUTC = DateTime.UtcNow,
					Data = JsonSerializer.Serialize( new ProductUpdatedContract
					{
						Id = updatedProduct.Id,
						Name = updatedProduct.Name,
						Price = updatedProduct.Price,
					} ),
					IsProcessed = false
				}, cancellationToken );
				await dbContext.SaveChangesAsync( cancellationToken );
				await transaction.CommitAsync( cancellationToken );
				return new Response<string>( ResponseMessage.ProductUpdated );
			}
		}
		catch
		{
			await transaction.RollbackAsync( cancellationToken );
			return new Response<string>( ResponseMessage.Failure );
		}
		return new Response<string>( ResponseMessage.Failure );
	}



	public async Task<Response<ProductDto>> GetProductByIdAsync (long id, CancellationToken cancellationToken)
	{
		var product = await productRepository.GetByIdAsync( id, cancellationToken );
		if (product is null)
			return new Response<ProductDto>( ResponseMessage.ProductNotFound, false );

		ProductDto productDto = mapper.Map<ProductDto>( product );
		AddLinksForProduct( productDto );
		return new Response<ProductDto>( productDto, ResponseMessage.Success );
	}

	private void AddLinksForProduct (ProductDto productDto)
	{
		productDto.Links.Add( linkService.GenerateLinks(
			   "GetProduct",
			   new { id = productDto.Id },
			   "self",
			   "GET" ) );

		productDto.Links.Add( linkService.GenerateLinks(
			  "DeleteProduct",
			  new { id = productDto.Id },
			  "delete-product",
			  "DELETE" ) );

		productDto.Links.Add( linkService.GenerateLinks(
			  "UpdateProduct",
			  new { id = productDto.Id },
			  "update-product",
			  "PUT" ) );

	}
}
