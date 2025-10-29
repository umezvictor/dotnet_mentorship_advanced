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

    [HttpDelete("{id}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await Mediator.Send(new DeleteProductCommand { Id = id }));
    }

    [HttpGet("{id}", Name = "GetProduct")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<ProductDto>), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await Mediator.Send(new GetProductByIdQuery { Id = id });
        if (response.Succeeded)
            return Ok(response);
        return NotFound(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(Response<List<ProductDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductsByCategoryId([FromQuery] GetProductsQuery query)
    {

        return Ok(await Mediator.Send(query));
    }

    [HttpPut("{id}", Name = "UpdateProduct")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateProductCommand command)
    {
        command.Id = id;
        return Ok(await Mediator.Send(command));
    }
}
