using Catalog.Application.Features.Products.Queries.GetProducts;
using Catalog.Core.Helper;
using Catalog.Core.Specifications;
using MediatR;

namespace Catalog.Application.Features.Products.Queries.GetProductsPaginated
{
    public class GetProductsPaginatedQuery : IRequest<Pagination<ProductResponse>>
    {
        public required CatalogSpecParams CatalogSpecParams { get; set; }

        public GetProductsPaginatedQuery(CatalogSpecParams catalogSpecParams)
        {
            CatalogSpecParams = catalogSpecParams ?? throw new ArgumentNullException(nameof(catalogSpecParams));
        }
    }
}
