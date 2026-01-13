using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;
public sealed class Outbox
{
	[Key]
	public int Id { get; set; }
	public string Data { get; set; } = null!;
	public string Status { get; set; } = string.Empty;
	public string CorrelationId { get; set; } = null!;
	public DateTime CreatedOnUTC { get; set; }

}
