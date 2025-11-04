using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Shared;
using Shared.Constants;
using Shared.Dto;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting(AppConstants.RateLimitingPolicy)]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{



    [HttpPost]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add([FromBody] AddCategoryRequest request, CancellationToken cancellationToken)
    {

        return Ok(await categoryService.AddCategoryAsync(request, cancellationToken));
    }

    [HttpDelete("{id}", Name = "DeleteCategory")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        return Ok(await categoryService.DeleteCategoryAsync(new DeleteCategoryRequest { Id = id }, cancellationToken));
    }

    [HttpGet("{id}", Name = "GetCategory")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        return Ok(await categoryService.GetCategoryById(id, cancellationToken));
    }


    [HttpGet]
    [ProducesResponseType(typeof(Response<List<CategoryDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await categoryService.GetCategoriesAsync(cancellationToken));
    }


    [HttpPut("{id}", Name = "UpdateCategory")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        request.Id = id;
        return Ok(await categoryService.UpdateCategoryAsync(request, cancellationToken));
    }


}
