using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstractions;
using Shared;
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
        //[Authorize]
        //if there is function that takes more than 3 parameters => take all of them in one class

        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetProducts([FromQuery] ProductQueryParameters queryParameters)
        {
            var products = await _serviceManager.ProductService.GetAllProducts(queryParameters);
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
          
            var product = await _serviceManager.ProductService.GetProductById(id);

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
