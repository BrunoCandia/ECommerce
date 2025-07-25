using Catalog.Application.Features.Products.Queries.GetProducts;
using MediatR;

namespace Catalog.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
        public required string Name { get; set; }

        public required string Summary { get; set; }

        public required string Description { get; set; }

        public required string ImageFile { get; set; }

        public BrandRequest? Brands { get; set; }

        public TypeRequest? Types { get; set; }

        public required decimal Price { get; set; }
    }
}
