namespace Catalog.Application.Features.Products.Commands.CreateProduct
{
    public class BrandRequest
    {
        public required string Id { get; set; }

        public required string Name { get; set; }
    }
}
