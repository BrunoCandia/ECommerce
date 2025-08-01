using AutoMapper;
using Discount.Core.Entities;
using Discount.Grpc.Protos;

namespace Discount.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Coupon, CouponModel>()
                .ForMember(dest => dest.AmountOff, opt => opt.MapFrom(src => src.AmountOff.ToString("F2")));

            CreateMap<CouponModel, Coupon>()
                .ForMember(dest => dest.AmountOff, opt => opt.MapFrom(src => decimal.Parse(src.AmountOff)));
        }
    }
}
