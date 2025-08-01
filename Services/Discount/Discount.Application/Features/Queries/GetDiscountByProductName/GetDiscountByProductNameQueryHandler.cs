using AutoMapper;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Features.Queries.GetDiscountByProductName
{
    public class GetDiscountByProductNameQueryHandler : IRequestHandler<GetDiscountByProductNameQuery, CouponModel>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public GetDiscountByProductNameQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CouponModel> Handle(GetDiscountByProductNameQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _discountRepository.GetDiscountByProductNameAsync(request.ProductName);

            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount for the Product Name: {request.ProductName} not found"));
            }

            return _mapper.Map<CouponModel>(coupon);
        }
    }
}
