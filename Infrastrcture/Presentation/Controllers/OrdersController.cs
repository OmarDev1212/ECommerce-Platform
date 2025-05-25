using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstractions;
using Shared.DTO.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]

    public class OrdersController(IServiceManager serviceManager) : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto createOrderDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await serviceManager.OrderService.CreateOrderAsync(email!, createOrderDto));
        }
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<DeliveryMethodDto>> GetAllDeliveryMethods()
        {
            return Ok(await serviceManager.OrderService.GetDeliveryMethods());
        }

        [HttpGet("AllOrdersForCuurentUser")]
        [Authorize]
        public async Task<ActionResult<OrderDto>> GetAllOrdersForCuurentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await serviceManager.OrderService.GetAllOrdersForCurrentUser(email!));
        }

        [HttpGet("{id:Guid}")]
        //[Authorize]
        public async Task<ActionResult<OrderDto>> GetAllOrderForCuurentUser(Guid id)
        {
            return Ok(await serviceManager.OrderService.GetOrderByIdForCurrentUser(id));
        }

    }
}
