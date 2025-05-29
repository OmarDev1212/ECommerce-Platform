using Shared.DTO.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractions
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(string basktedId);
        Task UpdateOrderStatus(string jsonRequest, string stripeHeader);
    }
}
