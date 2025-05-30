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
            var stripeSignatureHeader = Request.Headers["Stripe-Signature"];

            await serviceManager.paymentService.UpdateOrderStatus(json, stripeSignatureHeader!);
            return new EmptyResult();
        }

    }

}





