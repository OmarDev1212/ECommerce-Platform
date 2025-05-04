using Shared.DTO.ProductModule;

namespace ServiceAbstractions
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAllProducts();
        ProductDto GetProductById(int id);
        IEnumerable<BrandDto> GetBrands();
        IEnumerable<TypeDto> GetTypes();
    }
}
