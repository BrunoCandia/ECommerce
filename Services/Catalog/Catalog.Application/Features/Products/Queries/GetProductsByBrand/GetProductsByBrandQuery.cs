using Catalog.Application.Features.Products.Queries.GetProducts;
using MediatR;

namespace Catalog.Application.Features.Products.Queries.GetProductsByBrand
{
    public class GetProductsByBrandQuery : IRequest<IEnumerable<ProductResponse>>
    {
        public string Brand { get; set; }

        public GetProductsByBrandQuery(string brand)
        {
            Brand = brand ?? throw new ArgumentNullException(nameof(brand));
        }
    }
}
