using MongoDB.Bson.Serialization.Attributes;

namespace CartService.Domain
{
    //review constraints
    public sealed class Cart
    {
        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
