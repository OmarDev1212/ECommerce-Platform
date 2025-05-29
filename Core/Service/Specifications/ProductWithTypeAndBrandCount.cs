using DomainLayer.Models.ProductModule;
using Shared.DTO.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class ProductWithTypeAndBrandCount : BaseSpecification<Product, int>
    {
        public ProductWithTypeAndBrandCount(ProductQueryParameters queryParameters) : base(p =>

                ((!queryParameters.TypeId.HasValue || p.ProductTypeId == queryParameters.TypeId) &&
                (!queryParameters.BrandId.HasValue || p.ProductBrandId == queryParameters.BrandId) &&
                (string.IsNullOrEmpty(queryParameters.search) || p.Name.ToLower().Contains(queryParameters.search!.ToLower()))
            ))
        {

        }
    }
}
