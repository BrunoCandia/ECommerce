using MediatR;

namespace Basket.Application.Features.ShoppingCart.Commands.DeleteBasketByUserName
{
    public class DeleteBasketByUserNameCommand : IRequest<Unit>
    {
        public required string UserName { get; set; }

        public DeleteBasketByUserNameCommand(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
