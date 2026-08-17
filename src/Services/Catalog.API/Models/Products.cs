using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Models
{
    public class Products
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Summery { get; set; }
        public string ImageFile { get; set; }
        public Decimal Price { get; set; }

    }
}
