using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Features.Queries.GetDiscountByProductName
{
    public class GetDiscountByProductNameQuery : IRequest<CouponModel>
    {
        public required string ProductName { get; set; }

        public GetDiscountByProductNameQuery(string productName)
        {
            ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
        }
    }
}
