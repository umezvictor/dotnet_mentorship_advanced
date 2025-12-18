using System.ComponentModel.DataAnnotations;

namespace Shared.Dto;
public class AddProductRequest
{
    [Required]
    public required string Name { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public int CategoryId { get; set; }

}