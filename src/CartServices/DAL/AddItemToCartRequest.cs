using System.ComponentModel.DataAnnotations;
using DAL.Entities;

namespace DAL;
public class AddItemToCartRequest
{
	[Required]
	public string CartKey { get; set; }
	[Required]
	public CartItem CartItem { get; set; }

}


public class UpdateItemRequest
{
	public CartItem CartItem { get; set; }

}