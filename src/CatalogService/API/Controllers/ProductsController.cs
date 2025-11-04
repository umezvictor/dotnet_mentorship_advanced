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
public class ProductsController(IProductService productService) : ControllerBase
{


    [HttpPost]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add([FromBody] AddProductRequest request, CancellationToken cancellationToken)
    {
        return Ok(await productService.AddProductAsync(request, cancellationToken));

    }

    [HttpDelete("{id}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        return Ok(await productService.DeleteProductAsync(new DeleteProductRequest { Id = id }, cancellationToken));
    }

    [HttpGet("{id}", Name = "GetProduct")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<ProductDto>), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        var response = await productService.GetProductByIdAsync(id, cancellationToken);
        if (response.Succeeded)
            return Ok(response);
        return NotFound(response);
    }

    //get products by category id
    [HttpGet]
    [ProducesResponseType(typeof(Response<List<ProductDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductsByCategoryId([FromQuery] GetProductsQuery query, CancellationToken cancellationToken)
    {
        return Ok(await productService.GetProductsByCategoryIdAsync(query, cancellationToken));
    }


    [HttpPut("{id}", Name = "UpdateProduct")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateProductRequest command, CancellationToken cancellationToken)
    {
        command.Id = id;
        return Ok(await productService.UpdateProductAsync(command, cancellationToken));
    }
}
