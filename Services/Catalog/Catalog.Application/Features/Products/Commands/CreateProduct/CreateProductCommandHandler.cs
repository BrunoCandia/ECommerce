using AutoMapper;
using Catalog.Application.Features.Products.Queries.GetProducts;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<Product>(request);

            if (productEntity is null)
            {
                throw new ArgumentNullException(nameof(productEntity), "Product entity cannot be null");
            }

            var newProduct = await _productRepository.CreateProductAsync(productEntity);
            var productResponse = _mapper.Map<ProductResponse>(newProduct);

            return productResponse;
        }
    }
}
