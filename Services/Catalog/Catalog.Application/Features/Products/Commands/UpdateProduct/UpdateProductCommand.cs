using Catalog.Application.Features.Products.Commands.CreateProduct;
using MediatR;

namespace Catalog.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public required string Id { get; set; }

        public required string Name { get; set; }

        public required string Summary { get; set; }

        public required string Description { get; set; }

        public required string ImageFile { get; set; }

        public BrandRequest? Brands { get; set; }

        public TypeRequest? Types { get; set; }

        public required decimal Price { get; set; }
    }
}
