using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities
{
    public class Product : BaseEntity
    {
        [BsonElement("Name")]
        public required string Name { get; set; }

        public required string Summary { get; set; }

        public required string Description { get; set; }

        public required string ImageFile { get; set; }

        public ProductBrand? Brands { get; set; }

        public ProductType? Types { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public required decimal Price { get; set; }
    }
}
