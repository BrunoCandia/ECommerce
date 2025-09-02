using AutoMapper;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Features.Queries.GetDiscountByProductName
{
    public class GetDiscountByProductNameQueryHandler : IRequestHandler<GetDiscountByProductNameQuery, CouponModel>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDiscountByProductNameQueryHandler> _logger;

        public GetDiscountByProductNameQueryHandler(IDiscountRepository discountRepository, IMapper mapper, ILogger<GetDiscountByProductNameQueryHandler> logger)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CouponModel> Handle(GetDiscountByProductNameQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _discountRepository.GetDiscountByProductNameAsync(request.ProductName);

            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount for the Product Name: {request.ProductName} not found"));
            }

            _logger.LogInformation("Discount for the Product Name: {ProductName} is successfully retrieved", request.ProductName);

            return _mapper.Map<CouponModel>(coupon);
        }
    }
}
