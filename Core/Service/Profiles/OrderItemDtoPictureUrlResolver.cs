using AutoMapper;
using DomainLayer.Models.OrderAggregate;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.DTO.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profiles
{
    internal class OrderItemDtoPictureUrlResolver(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemDto, string>

    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!source.Product.PictureUrl.IsNullOrEmpty())
            {
                return $"{configuration["ApiBaseUrl"]}/{source.Product.PictureUrl}";
            }
            return null;
        }
    }
}
