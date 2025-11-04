using System.ComponentModel.DataAnnotations;

namespace Shared.Dto;

public class UpdateCategoryRequest
{
    [System.Text.Json.Serialization.JsonIgnore]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Image { get; set; } = string.Empty;
    public string ParentCategory { get; set; } = string.Empty;

}