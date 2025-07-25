using Catalog.Application.Features.Brands.Queries.GetBrands;
using Catalog.Application.Features.Types.Queries.GetTypes;

namespace Catalog.Application.Features.Products.Queries.GetProducts
{
    public class ProductResponse
    {
        public required string Id { get; set; }

        public required string Name { get; set; }

        public required string Summary { get; set; }

        public required string Description { get; set; }

        public required string ImageFile { get; set; }

        public BrandResponse? Brands { get; set; }

        public TypeResponse? Types { get; set; }

        public required decimal Price { get; set; }
    }
}
