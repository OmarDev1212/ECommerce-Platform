using Shared;
using Shared.DTO.ProductModule;

namespace ServiceAbstractions
{
    public interface IProductService
    {
        Task<PaginationResponse<ProductDto>> GetAllProducts(ProductQueryParameters queryParameters);
        Task<ProductDto> GetProductById(int id);
        Task<IEnumerable<BrandDto>> GetBrands();
        Task<IEnumerable<TypeDto>> GetTypes();
    }
}
