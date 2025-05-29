using DomainLayer.Models.ProductModule;
using Shared.DTO.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecification<Product, int>
    {
        //use this constructor to get product By Id
        //_dbContext.Products.Where(p=>p.Id==id).Include(p=>p.ProductType).Include(p=>p.ProductBrand)
        public ProductWithTypeAndBrandSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
        //use this constructor to GetAll Products 
        //_dbContext.Products.Include(p=>p.ProductType).Include(p=>p.ProductBrand)

        //we need to make filteration 
        //we can filter by ProductBrandId or ProductTypeId
        public ProductWithTypeAndBrandSpecification(ProductQueryParameters queryParameters) :
            base(p =>

                ((!queryParameters.TypeId.HasValue || p.ProductTypeId == queryParameters.TypeId) &&
                (!queryParameters.BrandId.HasValue || p.ProductBrandId == queryParameters.BrandId) &&
                (string.IsNullOrEmpty(queryParameters.search) || p.Name.ToLower().Contains(queryParameters.search!.ToLower()))
            ))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            switch (queryParameters.sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescinding(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescinding(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
            ApplyPagination(queryParameters.PageSize, queryParameters.pageNumber);
        }
    }
}
