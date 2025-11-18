using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;
public sealed class Outbox
{
    [Key]
    public int Id { get; set; }
    public string Data { get; set; } = null!;
    public bool IsProcessed { get; set; }
    public DateTime CreatedOnUTC { get; set; }

}
