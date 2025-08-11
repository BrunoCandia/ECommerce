using EventBus.Messages.Events;

namespace Basket.Application.Messages
{
    public interface IBasketEventBusPublisher
    {
        Task PublishBasketCheckoutAsync(BasketCheckoutEvent eventMsg);
    }
}
