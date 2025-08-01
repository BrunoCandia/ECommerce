using Discount.Application.Features.Commands.CreateDiscount;
using Discount.Application.Features.Commands.DeleteDiscount;
using Discount.Application.Features.Commands.UpdateDiscount;
using Discount.Application.Features.Queries.GetDiscountByProductName;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.API.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator _mediator;

        public DiscountService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var query = new GetDiscountByProductNameQuery(request.ProductName)
            {
                ProductName = request.ProductName ?? throw new ArgumentNullException(nameof(request.ProductName))
            };

            var coupon = await _mediator.Send(query);

            return coupon;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var command = new CreateDiscountCommand
            {
                ProductName = request.Coupon.ProductName,
                Description = request.Coupon.Description,
                AmountOff = request.Coupon.AmountOff
            };

            var coupon = await _mediator.Send(command);

            return coupon;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var command = new UpdateDiscountCommand
            {
                Id = request.Coupon.Id,
                ProductName = request.Coupon.ProductName,
                Description = request.Coupon.Description,
                AmountOff = request.Coupon.AmountOff
            };

            var coupon = await _mediator.Send(command);

            return coupon;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var command = new DeleteDiscountCommand(request.ProductName)
            {
                ProductName = request.ProductName
            };

            var isDeleted = await _mediator.Send(command);

            return new DeleteDiscountResponse { Success = isDeleted };
        }
    }
}
