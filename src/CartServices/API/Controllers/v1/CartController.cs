using Asp.Versioning;
using BLL.Dtos;
using BLL.Services;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.ResponseObjects;

namespace API.Controllers.v1
{
	/// <summary>
	/// API for managing cart operations. Use v1 or v2 in the url to specify the version.
	/// </summary>
	[ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiVersion("1.0")]
	[Authorize(AuthenticationSchemes = "Bearer")]
	public class CartController(ICartService cartService) : ControllerBase
	{

		/// <summary>
		/// Add item to cart.
		/// </summary>
		/// <param name="request">Cart item data to be added.</param>
		/// <param name="cancellationToken"></param>
		/// <returns>Returns a success response.</returns>
		[HttpPost]
		[ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		[Authorize(Policy = "ManagerOrCustomerPolicy")]
		public async Task<IActionResult> AddItemToCartV1([FromBody] AddItemToCartRequest request, CancellationToken cancellationToken)
		{
			if (await cartService.AddItemToCartAsync(request, cancellationToken))
			{
				return Ok(new Response<string>(ResponseMessage.ItemAddedToCart));
			}

			return BadRequest(new Response<string>(ResponseMessage.ItemNotAddedToCart, false));
		}


		/// <summary>
		/// Deletes an item from the cart.
		/// </summary>
		/// <param name="id">The ID of the cart item to delete.</param>
		/// <param name="cartKey">The unique key identifying the cart.</param>
		/// <param name="cancellationToken">A token to cancel the operation.</param>
		/// <returns>Returns a success response if the item was removed; otherwise, a bad request response.</returns>
		[HttpDelete]
		[ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		[Authorize(Policy = "ManagerOrCustomerPolicy")]
		public async Task<IActionResult> DeleteCartItemV1([FromRoute] int id, [FromRoute] string cartKey, CancellationToken cancellationToken)
		{
			if (await cartService.DeleteCartItemAsync(new DeleteItemFromCartRequest { Id = id, CartKey = cartKey }, cancellationToken))
			{
				return Ok(new Response<string>(ResponseMessage.ItemRemovedFromCart));
			}

			return BadRequest(new Response<string>(ResponseMessage.ItemNotRemoved));
		}



		/// <summary>
		/// Retrieves the cart information for the specified cart key.
		/// </summary>
		/// <param name="cartKey">The unique key identifying the cart.</param>
		/// <param name="cancellationToken">A token to cancel the operation.</param>
		/// <returns>Returns the cart information if found; otherwise, a not found response.</returns>
		[HttpGet("{cartKey}")]
		[ProducesResponseType(typeof(Response<Cart>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		[Authorize(Policy = "ManagerOrCustomerPolicy")]
		public async Task<IActionResult> GetCartInfoV1([FromRoute] string cartKey, CancellationToken cancellationToken)
		{
			var cart = await cartService.GetCartItemsAsync(cartKey, cancellationToken);
			if (cart != null)
			{
				return Ok(new Response<Cart>(cart, ResponseMessage.ItemsFetched));
			}

			return NotFound();
		}


	}
}
