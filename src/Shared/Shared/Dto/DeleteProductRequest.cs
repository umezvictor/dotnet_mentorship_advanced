using System.ComponentModel.DataAnnotations;

namespace Shared.Dto;
public class DeleteProductRequest
{
	[Required]
	public int Id { get; set; }
}