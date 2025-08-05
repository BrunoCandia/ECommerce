using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Exceptions;
using Order.Core.Repositories;

namespace Order.Application.Features.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);

            if (orderToUpdate is null)
            {
                throw new OrderNotFoundException(nameof(Order), request.Id);
            }

            var orderToUpdateEntity = _mapper.Map(request, orderToUpdate);

            await _orderRepository.UpdateAsync(orderToUpdateEntity);

            _logger.LogInformation("Order with Id {Id} successfully updated", request.Id);

            return Unit.Value;
        }
    }
}
