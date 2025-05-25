using AutoMapper;
using DomainLayer.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.DTO.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profiles
{
    public class ProductPictureurlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!source.PictureUrl.IsNullOrEmpty())
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";
            }
            return null;
        }
    }
}
