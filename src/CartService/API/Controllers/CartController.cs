using BLL.Features.Add;
using BLL.Features.Delete;
using BLL.Features.GetAll;
using CartService.Shared.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;


namespace CartService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>() ?? null!;


        [HttpPost]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddItemToCart([FromBody] AddToCartCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteItemFromCartCommand { Id = id }));
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<List<CartDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetItems()
        {
            return Ok(await Mediator.Send(new GetCartItemsQuery()));
        }
    }
}
