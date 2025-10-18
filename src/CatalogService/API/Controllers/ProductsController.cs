using BLL.Features.Products.Add;
using BLL.Features.Products.Delete;
using BLL.Features.Products.GetAll;
using BLL.Features.Products.GetById;
using BLL.Features.Products.Update;
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
public class ProductsController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>() ?? null!;


    [HttpPost]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add([FromBody] AddProductCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await Mediator.Send(new DeleteProductCommand { Id = id }));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await Mediator.Send(new GetProductByIdQuery { Id = id }));
    }

    [HttpGet]
    [ProducesResponseType(typeof(Response<List<ProductDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await Mediator.Send(new GetProductsQuery()));
    }

    [HttpPut]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
