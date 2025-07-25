using MediatR;

namespace Catalog.Application.Features.Brands.Queries.GetBrands
{
    public class GetBrandsQuery : IRequest<IEnumerable<BrandResponse>>
    {
    }
}
