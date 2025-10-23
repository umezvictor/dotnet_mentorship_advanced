using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Models;
public sealed class Cart
{
    [BsonId]
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Image { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }


    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Name)
            && Price > 0
            && Id > 0;
    }

}
