using AutoMapper;
using Catalog.Application.Features.Brands.Queries.GetBrands;
using Catalog.Application.Features.Products.Commands.CreateProduct;
using Catalog.Application.Features.Products.Queries.GetProducts;
using Catalog.Application.Features.Types.Queries.GetTypes;
using Catalog.Core.Entities;

namespace Catalog.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductBrand, BrandResponse>().ReverseMap();
            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<ProductType, TypeResponse>().ReverseMap();

            CreateMap<CreateProductCommand, Product>().ReverseMap();

            ////CreateMap<Product, CreateProductCommand>().ReverseMap();

            ////CreateMap<CreateProductCommand, Product>()
            ////    .ForMember(dest => dest.Id, opt => opt.Ignore())
            ////    .ForMember(dest => dest.Brands, opt => opt.MapFrom(src => src.Brands))
            ////    .ForMember(dest => dest.Types, opt => opt.MapFrom(src => src.Types))
            ////    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        }
    }
}
