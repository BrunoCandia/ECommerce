using AutoMapper;
using Catalog.Application.Features.Products.Queries.GetProducts;
using Catalog.Core.Helper;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Features.Products.Queries.GetProductsPaginated
{
    public class GetProductsPaginatedQueryHandler : IRequestHandler<GetProductsPaginatedQuery, Pagination<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsPaginatedQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Pagination<ProductResponse>> Handle(GetProductsPaginatedQuery request, CancellationToken cancellationToken)
        {
            var productsPaginated = await _productRepository.GetProductsAsync(request.CatalogSpecParams);

            var productResponse = _mapper.Map<Pagination<ProductResponse>>(productsPaginated);

            return productResponse;
        }
    }
}
