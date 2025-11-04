namespace Shared.Dto;
public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string ParentCategory { get; set; } = string.Empty;
    public List<Link> Links { get; set; } = new();
}
