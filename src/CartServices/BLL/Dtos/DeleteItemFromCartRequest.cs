using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos;
public sealed class DeleteItemFromCartRequest
{
    [Required]
    public int Id { get; set; }
    [Required]
    public required string CartKey { get; set; }
}
