﻿using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Features.ShoppingCart.Commands.DeleteBasketByUserName
{
    public class DeleteBasketByUserNameCommandHandler : IRequestHandler<DeleteBasketByUserNameCommand, Unit>
    {
        private readonly IBasketRepository _basketRepository;

        public DeleteBasketByUserNameCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        }

        public async Task<Unit> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
        {
            await _basketRepository.DeleteBasketAsync(request.UserName);

            return Unit.Value;
        }
    }
}
