namespace DAL.Entities;
public class CartItem
{
    public int Id { get; set; } //refers to Product Id
    public required string Name { get; set; }
    public string Image { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
