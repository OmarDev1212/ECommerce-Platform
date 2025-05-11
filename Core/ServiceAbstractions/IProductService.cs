using Shared.DTO.ProductModule;

namespace ServiceAbstractions
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts(ProductQueryParameters queryParameters);
        Task<ProductDto> GetProductById(int id);
        Task<IEnumerable<BrandDto>> GetBrands();
        Task<IEnumerable<TypeDto>> GetTypes();
    }
}
