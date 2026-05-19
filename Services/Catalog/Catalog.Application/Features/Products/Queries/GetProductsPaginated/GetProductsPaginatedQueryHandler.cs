using AutoMapper;
using Catalog.Application.Features.Products.Queries.GetProducts;
using Catalog.Core.Helper;
using Catalog.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Features.Products.Queries.GetProductsPaginated
{
    public class GetProductsPaginatedQueryHandler : IRequestHandler<GetProductsPaginatedQuery, Pagination<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductsPaginatedQueryHandler> _logger;

        public GetProductsPaginatedQueryHandler(IProductRepository productRepository, IMapper mapper, ILogger<GetProductsPaginatedQueryHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Pagination<ProductResponse>> Handle(GetProductsPaginatedQuery request, CancellationToken cancellationToken)
        {
            var productsPaginated = await _productRepository.GetProductsAsync(request.CatalogSpecParams);

            var productResponse = _mapper.Map<Pagination<ProductResponse>>(productsPaginated);

            _logger.LogInformation("Retrieved {Count} products for page {PageIndex} with page size {PageSize}.", productResponse.Data.Count, productResponse.PageIndex, productResponse.PageSize);

            return productResponse;
        }
    }
}
