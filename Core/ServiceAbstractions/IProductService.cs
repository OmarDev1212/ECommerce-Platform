using Shared.DTO.ProductModule;

namespace ServiceAbstractions
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts(int? brandId,int? typeId,ProductSortingOptions options);
        Task<ProductDto> GetProductById(int id);
        Task<IEnumerable<BrandDto>> GetBrands();
        Task<IEnumerable<TypeDto>> GetTypes();
    }
}
