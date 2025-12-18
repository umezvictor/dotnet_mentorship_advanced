using System.ComponentModel.DataAnnotations;

namespace Shared.Dto;
public class UpdateProductRequest
{
	[System.Text.Json.Serialization.JsonIgnore]
	public long Id { get; set; }
	[Required]
	public required string Name { get; set; }
	public string Image { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	[Required]
	public decimal Price { get; set; }
	[Required]
	public int Amount { get; set; }
}
