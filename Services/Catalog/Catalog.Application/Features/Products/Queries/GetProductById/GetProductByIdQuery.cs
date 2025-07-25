using Catalog.Application.Features.Products.Queries.GetProducts;
using MediatR;

namespace Catalog.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductResponse>
    {
        public required string Id { get; set; }

        public GetProductByIdQuery(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }
    }
}
