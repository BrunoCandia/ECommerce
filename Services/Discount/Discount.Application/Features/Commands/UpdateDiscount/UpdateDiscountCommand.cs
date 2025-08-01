using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Features.Commands.UpdateDiscount
{
    public class UpdateDiscountCommand : IRequest<CouponModel>
    {
        public required int Id { get; set; }

        public required string ProductName { get; set; }

        public required string Description { get; set; }

        public required string AmountOff { get; set; }
    }
}
