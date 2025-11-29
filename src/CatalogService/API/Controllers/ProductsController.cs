using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Shared.Constants;
using Shared.Dto;
using Shared.ResponseObjects;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting(AppConstants.RateLimitingPolicy)]
public class ProductsController(IProductService productService) : ControllerBase
{


    [HttpPost]
    [ProducesResponseType(typeof(Response<long>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<long>), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    [Authorize(Policy = "ManagerPolicy")]
    public async Task<IActionResult> Add([FromBody] AddProductRequest request, CancellationToken cancellationToken)
    {
        var response = await productService.AddProductAsync(request, cancellationToken);
        if (response.Succeeded)
            return Ok(response);
        return BadRequest(response);

    }

    [HttpDelete("{id}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    [Authorize(Policy = "ManagerPolicy")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var response = await productService.DeleteProductAsync(new DeleteProductRequest { Id = id }, cancellationToken);
        if (response.Succeeded)
            return Ok(response);
        return BadRequest(response);
    }

    [HttpGet("{id}", Name = "GetProduct")]
    [ProducesResponseType(typeof(Response<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<ProductDto>), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Authorize(Policy = "ManagerOrCustomerPolicy")]
    public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        var response = await productService.GetProductByIdAsync(id, cancellationToken);
        if (response.Succeeded)
            return Ok(response);
        return NotFound(response);
    }


    [HttpGet]
    [ProducesResponseType(typeof(Response<PaginatedResponse<List<ProductDto>>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<PaginatedResponse<List<ProductDto>>>), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Authorize(Policy = "ManagerOrCustomerPolicy")]
    public async Task<IActionResult> GetProductsByCategoryId([FromQuery] GetProductsQuery query, CancellationToken cancellationToken)
    {
        var response = await productService.GetProductsByCategoryIdAsync(query, cancellationToken);
        if (response.Succeeded)
            return Ok(response);
        return NotFound(response);
    }


    [HttpPut("{id}", Name = "UpdateProduct")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    [Authorize(Policy = "ManagerPolicy")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        request.Id = id;
        var response = await productService.UpdateProductAsync(request, cancellationToken);
        if (response.Succeeded)
            return Ok(response);
        return BadRequest(response);
    }
}
