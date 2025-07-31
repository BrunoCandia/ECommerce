using AutoMapper;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Features.ShoppingCart.Queries.GetBasketByUserName
{
    public class GetBasketByUserNameQueryHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public GetBasketByUserNameQueryHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _basketRepository.GetBasketAsync(request.UserName);
            var shoppingCartResponse = _mapper.Map<ShoppingCartResponse>(shoppingCart);

            //TODO: Review, this can retun null if the user has no shopping cart

            return shoppingCartResponse;
        }
    }
}
