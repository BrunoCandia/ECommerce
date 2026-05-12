using EventBus.Messages.Events;
using MassTransit;

namespace Payment.API.EventBusConsumer
{
    public class OrderCheckoutConsumer : IConsumer<OrderCheckoutEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OrderCheckoutConsumer> _logger;

        public OrderCheckoutConsumer(IPublishEndpoint publishEndpoint, ILogger<OrderCheckoutConsumer> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCheckoutEvent> context)
        {
            var message = context.Message;

            _logger.LogInformation("Processing payment for Order Id: {OrderId}", message.Id);

            // Simulate payment processing logic here
            await Task.Delay(2000);

            // TODO: Implement the Inbox pattern to ensure idempotency and handle potential duplicate messages
            // Save the message to the database with a unique constraint on the OrderId to prevent processing the same order multiple times
            // Then create a BackgroundService that periodically checks for unprocessed messages and processes them, marking them as completed once done

            if (message.TotalPrice > 0)
            {
                var paymentCompletedEvent = new PaymentCompletedEvent
                {
                    OrderId = message.Id,
                    CorrelationId = context.CorrelationId.GetValueOrDefault(),
                    UserName = message.UserName ?? string.Empty,
                    TotalPrice = message.TotalPrice ?? 0,
                    CompletedAt = DateTimeOffset.UtcNow
                };

                await _publishEndpoint.Publish(paymentCompletedEvent);

                _logger.LogInformation("Payment completed for Order Id: {OrderId} and Correlation Id: {CorrelationId}", message.Id, context.CorrelationId.GetValueOrDefault());
            }
            else
            {
                var paymentFailedEvent = new PaymentFailedEvent
                {
                    OrderId = message.Id,
                    CorrelationId = context.CorrelationId.GetValueOrDefault(),
                    UserName = message.UserName ?? string.Empty,
                    FailedAt = DateTimeOffset.UtcNow,
                    FailureReason = "Invalid total price, cannot be zero or negative"
                };

                await _publishEndpoint.Publish(paymentFailedEvent);

                _logger.LogWarning("Payment failed for Order Id: {OrderId} and Correlation Id: {CorrelationId}. Reason: {Reason}", message.Id, context.CorrelationId.GetValueOrDefault(), paymentFailedEvent.FailureReason);
            }
        }
    }
}
