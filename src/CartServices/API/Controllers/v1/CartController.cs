using Asp.Versioning;
using BLL.Dtos;
using BLL.Services;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CartController(ICartService cartService) : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddItemToCartV1([FromBody] AddItemToCartRequest request, CancellationToken cancellationToken)
        {
            if (await cartService.AddItemToCartAsync(request, cancellationToken))
                return Ok(new Response<string>(ResponseMessage.ItemAddedToCart));
            return BadRequest(new Response<string>(ResponseMessage.ItemNotAddedToCart, false));
        }


        [HttpDelete]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteCartItemV1([FromRoute] int id, [FromRoute] string cartKey, CancellationToken cancellationToken)
        {
            if (await cartService.DeleteCartItemAsync(new DeleteItemFromCartRequest { Id = id, CartKey = cartKey }, cancellationToken))
                return Ok(new Response<string>(ResponseMessage.ItemRemovedFromCart));
            return BadRequest(new Response<string>(ResponseMessage.ItemNotRemoved));
        }



        [HttpGet("{cartKey}")]
        [ProducesResponseType(typeof(Response<Cart>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetCartInfoV1([FromRoute] string cartKey, CancellationToken cancellationToken)
        {
            var cart = await cartService.GetCartItemsAsync(cartKey, cancellationToken);
            if (cart != null)
                return Ok(new Response<Cart>(cart, ResponseMessage.ItemsFetched));
            return NotFound();
        }


    }
}
