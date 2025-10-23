using API;
using BLL.Features.Add;
using BLL.Features.Delete;
using BLL.Features.GetAll;
using DAL.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Shared.Constants;


namespace CartService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting(AppConstants.RateLimitingPolicy)]
    public class CartController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>() ?? null!;


        [HttpPost]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddItemToCart([FromBody] AddToCartCommand command)
        {

            await Mediator.Send(command);
            return Ok(new Response<string>(ResponseMessage.ItemAddedToCart));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            if (await Mediator.Send(new DeleteItemFromCartCommand { Id = id }))
                return Ok(new Response<string>(ResponseMessage.ItemRemovedFromCart));
            return BadRequest(new Response<string>(ResponseMessage.ItemNotRemoved));
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<List<CartDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetItems()
        {
            var cartItems = await Mediator.Send(new GetCartItemsQuery());
            if (cartItems.Count() > 0 && cartItems != null)
                return Ok(new Response<List<CartDto>>(cartItems, ResponseMessage.ItemsFetched));
            return NotFound();
        }
    }
}
