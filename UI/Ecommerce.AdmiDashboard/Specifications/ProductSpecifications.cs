using DomainLayer.Models.ProductModule;
using Service.Specifications;

namespace Ecommerce.AdminDashboard.Specifications
{
    public class ProductSpecifications: BaseSpecification<Product, int>
    {
        public ProductSpecifications()
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
