using MediatR;

namespace Discount.Application.Features.Commands.DeleteDiscount
{
    public class DeleteDiscountCommand : IRequest<bool>
    {
        public required string ProductName { get; set; }

        public DeleteDiscountCommand(string productName)
        {
            ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
        }
    }
}
