using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Service.Specifications;
using ServiceAbstractions;
using Shared.DTO.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<ProductDto>> GetAllProducts(int? brandId,int?typeId)
        {
            var specification = new ProductWithTypeAndBrandSpecification(brandId,typeId);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAll(specification);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<BrandDto>> GetBrands()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAll();
            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var specification = new ProductWithTypeAndBrandSpecification(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetById(specification);
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<IEnumerable<TypeDto>> GetTypes()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAll();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
        }
    }
}
