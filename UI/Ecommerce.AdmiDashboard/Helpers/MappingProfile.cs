using AutoMapper;
using DomainLayer.Models.ProductModule;
using Ecommerce.AdminDashboard.ViewModels;
using Shared.DTO.ProductModule;

namespace Ecommerce.AdminDashboard.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<ProductType,TypeDto>().ReverseMap();
            CreateMap<ProductBrand,BrandDto>().ReverseMap();
        }
    }
}
    