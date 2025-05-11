using Microsoft.AspNetCore.Mvc;
using ServiceAbstractions;
using Shared.DTO.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts([FromQuery] int?brandId, [FromQuery] int? typeId)
        {
            var products = await _serviceManager.ProductService.GetAllProducts(brandId,typeId);
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            if (id == 0) return BadRequest();

            var product = await _serviceManager.ProductService.GetProductById(id);

            if (product is null) return NotFound();
            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<BrandDto>> GetBrands()
        {
            var brands = await _serviceManager.ProductService.GetBrands();
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<TypeDto>> GetTypes()
        {
            var types = await _serviceManager.ProductService.GetTypes();
            return Ok(types);
        }
    }
}
