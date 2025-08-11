using AutoMapper;
using EventBus.Messages.Events;
using Order.Application.Features.Commands.CheckoutOrder;
using Order.Application.Features.Commands.UpdateOrder;
using Order.Application.Features.Queries.GetOrdersByUserName;

namespace Order.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Core.Entities.Order, OrderResponse>().ReverseMap();

            CreateMap<Core.Entities.Order, CheckoutOrderCommand>().ReverseMap();
            CreateMap<Core.Entities.Order, UpdateOrderCommand>().ReverseMap();

            CreateMap<BasketCheckoutEvent, CheckoutOrderCommand>().ReverseMap();
        }
    }
}
