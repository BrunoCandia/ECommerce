using EventBus.Messages.Events;
using MassTransit;
using Order.Core.Entities;
using Order.Core.Repositories;

namespace Order.API.EventBusConsumer
{
    public class PaymentFailedConsumer : IConsumer<PaymentFailedEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<PaymentFailedConsumer> _logger;

        public PaymentFailedConsumer(IOrderRepository orderRepository, ILogger<PaymentFailedConsumer> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            var order = await _orderRepository.GetByIdAsync(context.Message.OrderId);

            if (order is null)
            {
                _logger.LogError("Order not found for: Order Id {OrderId} and Correlation Id {CorrelationId}.", context.Message.OrderId, context.Message.CorrelationId);
                //_logger.LogError("Order not found for: Order Id {OrderId} and Correlation Id {CorrelationId}.", context.Message.OrderId, context.CorrelationId);

                return;
            }

            order.Status = OrderStatus.Failed;
            await _orderRepository.UpdateAsync(order);

            _logger.LogError("Payment failed for Order Id {OrderId} and Correlation Id {CorrelationId}. Reason: {FailureReason}", context.Message.OrderId, context.Message.CorrelationId, context.Message.FailureReason);
        }
    }
}
