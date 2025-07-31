using MediatR;

namespace Basket.Application.Features.ShoppingCart.Queries.GetBasketByUserName
{
    public class GetBasketByUserNameQuery : IRequest<ShoppingCartResponse>
    {
        public required string UserName { get; set; }

        public GetBasketByUserNameQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
