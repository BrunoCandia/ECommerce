using Basket.Application.Features.ShoppingCart.Queries.GetBasketByUserName;
using MediatR;

namespace Basket.Application.Features.ShoppingCart.Commands.CreateShoppingCart
{
    public class CreateShoppingCartCommand : IRequest<ShoppingCartResponse>
    {
        public required string UserName { get; set; }
        public List<ShoppingCartItemRequest> Items { get; set; }

        public CreateShoppingCartCommand(string userName, List<ShoppingCartItemRequest> items)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Items = items;
        }
    }
}
