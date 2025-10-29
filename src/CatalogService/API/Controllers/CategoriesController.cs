using BLL.Features.Categories.Add;
using BLL.Features.Categories.Delete;
using BLL.Features.Categories.GetAll;
using BLL.Features.Categories.GetById;
using BLL.Features.Categories.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Shared;
using Shared.Constants;
using Shared.Dto;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting(AppConstants.RateLimitingPolicy)]
public class CategoriesController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>() ?? null!;

    //add new category
    [HttpPost]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add([FromBody] AddCategoryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await Mediator.Send(new DeleteCategoryCommand { Id = id }));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await Mediator.Send(new GetCategoryQuery { Id = id }));
    }

    //list all categories
    [HttpGet]
    [ProducesResponseType(typeof(Response<List<CategoryDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await Mediator.Send(new GetCategoriesQuery()));
    }

    //update category
    [HttpPut]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
