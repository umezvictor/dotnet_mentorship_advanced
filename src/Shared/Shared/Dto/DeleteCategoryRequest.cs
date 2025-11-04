using System.ComponentModel.DataAnnotations;

namespace Shared.Dto;
public class DeleteCategoryRequest
{
    [Required]
    public int Id { get; set; }
}