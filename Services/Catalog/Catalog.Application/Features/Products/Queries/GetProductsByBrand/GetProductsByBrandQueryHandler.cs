using AutoMapper;
using Catalog.Application.Features.Products.Queries.GetProducts;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Features.Products.Queries.GetProductsByBrand
{
    public class GetProductsByBrandQueryHandler : IRequestHandler<GetProductsByBrandQuery, IEnumerable<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsByBrandQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ProductResponse>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsByBrandAsync(request.Brand);
            var productResponses = _mapper.Map<IEnumerable<ProductResponse>>(products);

            return productResponses;
        }
    }
}
