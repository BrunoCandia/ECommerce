using AutoMapper;
using Basket.Application.Features.ShoppingCart.Queries.GetBasketByUserName;
using Basket.Core.Repositories;
using MediatR;
using Entitites = Basket.Core.Entities;

namespace Basket.Application.Features.ShoppingCart.Commands.CreateShoppingCart
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public CreateShoppingCartCommandHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            //TODO: Integrate with Discount Service

            var shoppingCartEntity = _mapper.Map<Entitites.ShoppingCart>(request);

            if (shoppingCartEntity is null)
            {
                throw new ArgumentNullException(nameof(shoppingCartEntity), "Shopping cart entity cannot be null");
            }

            var newShoppingCart = await _basketRepository.UpsertBasketAsync(shoppingCartEntity);

            //TODO: Review, this can retun null

            var shoppingCartResponse = _mapper.Map<ShoppingCartResponse>(newShoppingCart);

            return shoppingCartResponse;
        }
    }
}
