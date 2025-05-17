using AutoMapper;
using DomainLayer.Models.BasketModule;
using Shared.DTO.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profiles
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket,CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem,BasketItemDto>().ReverseMap();
        }
    }
}
