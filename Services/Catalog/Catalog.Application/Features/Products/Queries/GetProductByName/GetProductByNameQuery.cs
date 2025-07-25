using Catalog.Application.Features.Products.Queries.GetProducts;
using MediatR;

namespace Catalog.Application.Features.Products.Queries.GetProductByName
{
    public class GetProductByNameQuery : IRequest<IEnumerable<ProductResponse>>
    {
        public required string Name { get; set; }

        public GetProductByNameQuery(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
