using System.ComponentModel.DataAnnotations;
using DAL.Entities;

namespace DAL;
public class AddItemToCartRequest
{
	[Required]
	public required string CartKey { get; set; }
	[Required]
	public required CartItem CartItem { get; set; }

}


public class UpdateItemRequest
{
	public required CartItem CartItem { get; set; }

}