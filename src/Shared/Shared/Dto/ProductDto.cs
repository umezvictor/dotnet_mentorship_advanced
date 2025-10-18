namespace Shared.Dto;
public class ProductDto
{

    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public string Image { get; set; } = string.Empty;


}

