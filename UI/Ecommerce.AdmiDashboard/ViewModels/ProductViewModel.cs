using DomainLayer.Models.ProductModule;

namespace Ecommerce.AdminDashboard.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? PictureUrl { get; set; }
        public IFormFile? Image { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public ProductBrand? ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
        public ProductType? ProductType { get; set; }
        public int ProductTypeId { get; set; }
    }
}
