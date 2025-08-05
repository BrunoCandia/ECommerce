using AutoMapper;
using MediatR;
using Order.Core.Repositories;

namespace Order.Application.Features.Queries.GetOrdersByUserName
{
    public class GetOrdersByUserNameQueryHandler : IRequestHandler<GetOrdersByUserNameQuery, IEnumerable<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersByUserNameQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersByUserNameQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersByUserNameAsync(request.UserName);

            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }
    }
}
