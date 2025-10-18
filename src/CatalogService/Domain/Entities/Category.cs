using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public sealed class Category : AuditableEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = new List<Product>();
}


