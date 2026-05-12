using EventBus.Messages.Events;
using MassTransit;
using Order.Core.Entities;
using Order.Core.Repositories;

namespace Order.API.EventBusConsumer
{
    public class PaymentCompletedConsumer : IConsumer<PaymentCompletedEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<PaymentCompletedConsumer> _logger;

        public PaymentCompletedConsumer(IOrderRepository orderRepository, ILogger<PaymentCompletedConsumer> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            var order = await _orderRepository.GetByIdAsync(context.Message.OrderId);

            if (order is null)
            {
                _logger.LogError("Order not found for: Order Id {OrderId} and Correlation Id {CorrelationId}.", context.Message.OrderId, context.Message.CorrelationId);
                //_logger.LogError("Order not found for: Order Id {OrderId} and Correlation Id {CorrelationId}.", context.Message.OrderId, context.CorrelationId);

                return;
            }

            order.Status = OrderStatus.Paid;
            await _orderRepository.UpdateAsync(order);

            _logger.LogInformation("Order Id {OrderId} with Correlation Id {CorrelationId} has been updated to Paid.", context.Message.OrderId, context.Message.CorrelationId);
        }
    }
}
