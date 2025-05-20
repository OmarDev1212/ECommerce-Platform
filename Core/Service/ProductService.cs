using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.ProductModule;
using Service.Specifications;
using ServiceAbstractions;
using Shared;
using Shared.DTO.ProductModule;
using Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<PaginationResponse<ProductDto>> GetAllProducts(ProductQueryParameters queryParameters)
        {
            var specification = new ProductWithTypeAndBrandSpecification(queryParameters);
            var countSpecification = new ProductWithTypeAndBrandCount(queryParameters);
            var repo = _unitOfWork.GetRepository<Product, int>();
            var products = await repo.GetAll(specification);
            var mappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            var totalCount = await repo.CountAsync(countSpecification);
            return new PaginationResponse<ProductDto>(queryParameters.PageIndex, queryParameters.PageSize, totalCount, mappedProducts);
        }

        public async Task<IEnumerable<BrandDto>> GetBrands()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAll();
            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            if (id == 0) throw new ProductNotFoundException(id);
            var specification = new ProductWithTypeAndBrandSpecification(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetById(specification);
            return product is null ? throw new ProductNotFoundException(id) : _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<IEnumerable<TypeDto>> GetTypes()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAll();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
        }
    }
}
