using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Order.Application.Features.Commands.CheckoutOrder;

namespace Order.API.EventBusConsumer
{
    public class BasketOrderConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketOrderConsumer> _logger;

        public BasketOrderConsumer(IMediator mediator, IMapper mapper, ILogger<BasketOrderConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> consumeContext)
        {
            using var scope = _logger.BeginScope("Consuming Basket Order Checkout Event for {CorrelationId}", consumeContext.Message.CorrelationId);

            var command = _mapper.Map<CheckoutOrderCommand>(consumeContext.Message);

            var result = await _mediator.Send(command);

            _logger.LogInformation("Basket Order Checkout Event consumed successfully for User: {UserName}", consumeContext.Message.UserName);
        }
    }
}
