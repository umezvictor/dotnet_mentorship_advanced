using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;
public sealed class Product : AuditableEntity
{

	[Key]
	public int Id { get; set; }
	public required string Name { get; set; }
	public string Description { get; set; } = string.Empty;
	[Required]
	public decimal Price { get; set; }
	[Required]
	public int Amount { get; set; }
	public string Image { get; set; } = string.Empty;
	public int CategoryId { get; set; }
	public required Category Category { get; set; }
}
