﻿using AutoMapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Features.Commands.UpdateDiscount
{
    public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, CouponModel>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public UpdateDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CouponModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon = _mapper.Map<Coupon>(request);

            await _discountRepository.UpdateDiscountAsync(coupon);

            var couponModel = _mapper.Map<CouponModel>(coupon);

            return couponModel;
        }
    }
}
