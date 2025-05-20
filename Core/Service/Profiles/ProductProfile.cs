using AutoMapper;
using DomainLayer.Models.ProductModule;
using Shared.DTO.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, opt =>
                    opt.MapFrom(src => new Uri(new Uri("https://localhost:7069/"), src.PictureUrl).AbsoluteUri));
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }

    }
}
