using Microsoft.AspNetCore.Mvc;
using ServiceAbstractions;
using Shared.DTO.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BasketsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto?>> GetBasket(string Id)
        {
            return await _serviceManager.BasketService.GetCustomerBasket(Id);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto?>> CreateOrUpdateBasket(CustomerBasketDto basketDto)
        {
            return await _serviceManager.BasketService.CreateOrUpdateBasket(basketDto);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            return await _serviceManager.BasketService.DeleteBasket(id);
        }
    }
}
