using AutoMapper;
using DomainLayer.Models.Identity;
using DomainLayer.Models.OrderAggregate;
using Microsoft.Extensions.Configuration;
using Shared.DTO.IdentityModule;
using Shared.DTO.OrderModule;
using System.Runtime.CompilerServices;

namespace Service.Profiles
{
    internal class OrderProfile : Profile
    {

        public OrderProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.Product.ProductName))
                  //.ForMember(dest => dest.PictureUrl, opt =>
                  //    opt.MapFrom(src=>src.Product.PictureUrl));

                  .ForMember(dest => dest.PictureUrl, opt =>
                    opt.MapFrom<OrderItemDtoPictureUrlResolver>());
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Status, options => options.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.TotalPrice, options => options.MapFrom(src => src.GetTotal()))
                .ForMember(dest => dest.Items, options => options.MapFrom(src => src.Items))
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id.ToString()))
                .ForMember(dest=>dest.DeliveryMethod,options=>options.MapFrom(src=>src.DeliveryMethod.ShortName));

            CreateMap<DeliveryMethod, DeliveryMethodDto>();
        }
    }
}
