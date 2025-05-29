using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstractions;
using Shared.DTO.BasketModule;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class PaymentsController(IServiceManager serviceManager):ControllerBase
    {
        [HttpPost("{BasketId:guid}")]
        [Authorize]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string BasketId)
        {
            return await serviceManager.paymentService.CreateOrUpdatePaymentIntent(BasketId);
        }
        [HttpPost("WebHook")]
        public async Task<ActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            await serviceManager.paymentService.UpdateOrderStatus(json, Request.Headers["Stripe-Signature"]!);
            return new EmptyResult();
        }

    }

}





