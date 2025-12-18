using System.ComponentModel.DataAnnotations;

namespace Shared.Dto;
public class GetProductsQuery
{
	[Required]
	public int CategoryId { get; set; }
	public int PageNumber { get; set; }
	public int PageSize { get; set; }

}