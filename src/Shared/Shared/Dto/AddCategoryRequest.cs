using System.ComponentModel.DataAnnotations;

namespace Shared.Dto;
public class AddCategoryRequest
{
	[Required]
	public required string Name { get; set; }
	public string Image { get; set; } = string.Empty;
	public string ParentCategory { get; set; } = string.Empty;

}