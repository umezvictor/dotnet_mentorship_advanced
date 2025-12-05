using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Shared.Constants;
using Shared.Dto;
using Shared.ResponseObjects;

namespace API.Controllers;

[Route( "api/[controller]" )]
[ApiController]
[EnableRateLimiting( AppConstants.RateLimitingPolicy )]
public class CategoriesController (ICategoryService categoryService) : ControllerBase
{



	[HttpPost]
	[ProducesResponseType( typeof( Response<string> ), StatusCodes.Status200OK )]
	[ProducesResponseType( typeof( Response<string> ), StatusCodes.Status400BadRequest )]
	[ProducesDefaultResponseType]
	[Authorize( Policy = "ManagerPolicy" )]
	public async Task<IActionResult> Add ([FromBody] AddCategoryRequest request, CancellationToken cancellationToken)
	{

		var response = await categoryService.AddCategoryAsync( request, cancellationToken );
		if (response.Succeeded)
			return Ok( response );
		return BadRequest( response );
	}

	[HttpDelete( "{id}", Name = "DeleteCategory" )]
	[ProducesResponseType( typeof( Response<string> ), StatusCodes.Status200OK )]
	[ProducesResponseType( typeof( Response<string> ), StatusCodes.Status400BadRequest )]
	[ProducesDefaultResponseType]
	[Authorize( Policy = "ManagerPolicy" )]
	public async Task<IActionResult> Delete ([FromRoute] int id, CancellationToken cancellationToken)
	{

		var response = await categoryService.DeleteCategoryAsync( new DeleteCategoryRequest { Id = id }, cancellationToken );
		if (response.Succeeded)
			return Ok( response );
		return BadRequest( response );

	}

	[HttpGet( "{id}", Name = "GetCategory" )]
	[ProducesResponseType( typeof( Response<CategoryDto> ), StatusCodes.Status200OK )]
	[ProducesResponseType( typeof( Response<CategoryDto> ), StatusCodes.Status404NotFound )]
	[ProducesDefaultResponseType]
	[Authorize( Policy = "ManagerOrCustomerPolicy" )]
	public async Task<IActionResult> GetById (int id, CancellationToken cancellationToken)
	{
		var response = await categoryService.GetCategoryById( id, cancellationToken );
		if (response.Succeeded)
			return Ok( response );
		return NotFound( response );
	}


	[HttpGet]
	[ProducesResponseType( typeof( Response<List<CategoryDto>> ), StatusCodes.Status200OK )]
	[ProducesResponseType( typeof( Response<List<CategoryDto>> ), StatusCodes.Status404NotFound )]
	[ProducesDefaultResponseType]
	[Authorize( Policy = "ManagerOrCustomerPolicy" )]
	public async Task<IActionResult> GetAll (CancellationToken cancellationToken)
	{
		var response = await categoryService.GetCategoriesAsync( cancellationToken );
		if (response.Succeeded)
			return Ok( response );
		return NotFound( response );
	}


	[HttpPut( "{id}", Name = "UpdateCategory" )]
	[ProducesResponseType( typeof( Response<string> ), StatusCodes.Status200OK )]
	[ProducesResponseType( typeof( Response<string> ), StatusCodes.Status400BadRequest )]
	[ProducesDefaultResponseType]
	[Authorize( Policy = "ManagerPolicy" )]
	public async Task<IActionResult> Update ([FromRoute] int id, [FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
	{
		request.Id = id;
		var response = await categoryService.UpdateCategoryAsync( request, cancellationToken );
		if (response.Succeeded)
			return Ok( response );
		return BadRequest( response );
	}


}
