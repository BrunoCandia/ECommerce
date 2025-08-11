using AutoMapper;
using Basket.Application.Messages;
using Basket.Core.Repositories;
using EventBus.Messages.Events;
using MediatR;

namespace Basket.Application.Features.ShoppingCart.Commands.CheckoutBasket
{
    public class CheckoutBasketCommandHandler : IRequestHandler<CheckoutBasketCommand, Unit>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketEventBusPublisher _basketEventPublisher;
        private readonly IMapper _mapper;

        public CheckoutBasketCommandHandler(IBasketRepository basketRepository, IBasketEventBusPublisher basketEventPublisher, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _basketEventPublisher = basketEventPublisher;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CheckoutBasketCommand checkoutBasketCommand, CancellationToken cancellationToken)
        {
            // Get the basket data
            var shoppingCart = await _basketRepository.GetBasketAsync(checkoutBasketCommand.UserName);

            if (shoppingCart is null)
                throw new Exception($"Basket for user '{checkoutBasketCommand.UserName}' not found.");

            // Map to event/message and enrich with basket data
            var eventMsg = new BasketCheckoutEvent
            {
                // Map properties from command and basket
                UserName = checkoutBasketCommand.UserName,
                TotalPrice = checkoutBasketCommand.TotalPrice,
                FirstName = checkoutBasketCommand.FirstName,
                LastName = checkoutBasketCommand.LastName,
                EmailAddress = checkoutBasketCommand.EmailAddress,
                AddressLine = checkoutBasketCommand.AddressLine,
                Country = checkoutBasketCommand.Country,
                State = checkoutBasketCommand.State,
                ZipCode = checkoutBasketCommand.ZipCode,
                CardName = checkoutBasketCommand.CardName,
                CardNumber = checkoutBasketCommand.CardNumber,
                Expiration = checkoutBasketCommand.Expiration,
                Cvv = checkoutBasketCommand.Cvv,
                PaymentMethod = checkoutBasketCommand.PaymentMethod
            };

            await _basketEventPublisher.PublishBasketCheckoutAsync(eventMsg);

            // TODO: Handle if the message was sent successfully but failed to process in the consumer(Order) due to validations
            // and the basket was deleted.

            // Delete the basket
            await _basketRepository.DeleteBasketAsync(checkoutBasketCommand.UserName);

            return Unit.Value;
        }
    }
}
