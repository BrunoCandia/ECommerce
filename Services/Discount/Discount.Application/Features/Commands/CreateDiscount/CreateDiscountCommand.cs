using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Features.Commands.CreateDiscount
{
    public class CreateDiscountCommand : IRequest<CouponModel>
    {
        public required string ProductName { get; set; }

        public required string Description { get; set; }

        public required string AmountOff { get; set; }
    }
}
