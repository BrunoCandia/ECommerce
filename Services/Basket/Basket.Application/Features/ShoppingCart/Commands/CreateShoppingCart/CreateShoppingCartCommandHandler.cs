using AutoMapper;
using Basket.Application.Features.ShoppingCart.Queries.GetBasketByUserName;
using Basket.Application.GrpcServices;
using Basket.Core.Repositories;
using MediatR;
using Entitites = Basket.Core.Entities;

namespace Basket.Application.Features.ShoppingCart.Commands.CreateShoppingCart
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public CreateShoppingCartCommandHandler(IBasketRepository basketRepository, IMapper mapper, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
        }

        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            //Integrate with Discount Service
            foreach (var item in request.Items)
            {
                var coupon = await _discountGrpcService.GetDiscountAsync(item.ProductName);

                if (decimal.TryParse(coupon.AmountOff, out var amountOff))
                {
                    item.Price -= amountOff;
                }
            }

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
