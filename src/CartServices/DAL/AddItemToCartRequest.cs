using DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace DAL;
public class AddItemToCartRequest
{
    [Required]
    public string CartKey { get; set; }
    [Required]
    public CartItem CartItem { get; set; }

}