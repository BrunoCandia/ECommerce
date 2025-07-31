using AutoMapper;
using Basket.Application.Features.ShoppingCart.Commands.CreateShoppingCart;
using Basket.Application.Features.ShoppingCart.Queries.GetBasketByUserName;
using Basket.Core.Entities;

namespace Basket.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartResponse>().ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemResponse>().ReverseMap();

            CreateMap<CreateShoppingCartCommand, ShoppingCart>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();

            CreateMap<ShoppingCartItemRequest, ShoppingCartItem>().ReverseMap();
        }
    }
}
