using Basket.Application.Messages;
using EventBus.Messages.Events;
using MassTransit;

namespace Basket.Infrastructure.Messages
{
    public class BasketEventBusPublisher : IBasketEventBusPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketEventBusPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishBasketCheckoutAsync(BasketCheckoutEvent eventMsg)
        {
            await _publishEndpoint.Publish(eventMsg);
        }
    }
}
