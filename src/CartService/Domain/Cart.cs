using MongoDB.Bson.Serialization.Attributes;

namespace CartService.Domain
{
    public sealed class Cart
    {
        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Image { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
