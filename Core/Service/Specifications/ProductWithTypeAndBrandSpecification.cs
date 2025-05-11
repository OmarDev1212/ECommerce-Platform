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
        public ProductWithTypeAndBrandSpecification(int? brandId, int? typeId, ProductSortingOptions options) :
            base(p =>

                ((!typeId.HasValue || p.ProductTypeId == typeId) &&
                (!brandId.HasValue || p.ProductBrandId == brandId)
            ))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            switch (options)
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
                    AddOrderByDescinding(p=>p.Price);
                    break;
                default:
                    AddOrderBy(p=>p.Name);
                    break;
            }
        }
    }
}
