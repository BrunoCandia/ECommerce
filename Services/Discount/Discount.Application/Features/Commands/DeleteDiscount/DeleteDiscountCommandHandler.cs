using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Features.Commands.DeleteDiscount
{
    public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, bool>
    {
        private readonly IDiscountRepository _discountRepository;

        public DeleteDiscountCommandHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
        }

        public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var isDeleted = await _discountRepository.DeleteDiscountAsync(request.ProductName);

            return isDeleted;
        }
    }
}
