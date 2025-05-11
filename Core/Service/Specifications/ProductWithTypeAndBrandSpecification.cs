using DomainLayer.Models.ProductModule;
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
        public ProductWithTypeAndBrandSpecification(int? brandId, int? typeId) :
            base(p =>

                ((!typeId.HasValue || p.ProductTypeId == typeId) &&
                (!brandId.HasValue || p.ProductBrandId == brandId)
            ))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
