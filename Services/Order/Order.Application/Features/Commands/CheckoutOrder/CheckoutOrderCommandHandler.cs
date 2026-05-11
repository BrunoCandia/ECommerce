using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Constants;
using Order.Core.Repositories;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Order.Application.Features.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Guid> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Core.Entities.Order>(request);

            var generatedOrder = await _orderRepository.AddAsync(orderEntity);

            var jsonOptions = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };

            var outboxMessage = new Core.Entities.OutboxMessage
            {
                CorrelationId = request.CorrelationId,
                Type = OutboxMessageType.OrderCheckouted,
                OccuredOnUtcNow = DateTimeOffset.UtcNow,
                Content = JsonSerializer.Serialize(new
                {
                    generatedOrder.Id,
                    generatedOrder.UserName,
                    generatedOrder.TotalPrice,
                    generatedOrder.FirstName,
                    generatedOrder.LastName,
                    generatedOrder.AddressLine,
                    generatedOrder.Country,
                    generatedOrder.State,
                    generatedOrder.ZipCode,
                    generatedOrder.CardName,
                    generatedOrder.CardNumber,
                    generatedOrder.Expiration,
                    generatedOrder.Cvv,
                    generatedOrder.PaymentMethod,
                    generatedOrder.Status
                }, jsonOptions)
            };

            await _orderRepository.AddOutboxMessageAsync(outboxMessage);

            _logger.LogInformation("Order with Id {Id} successfully created", generatedOrder.Id);

            return generatedOrder.Id;
        }
    }
}
