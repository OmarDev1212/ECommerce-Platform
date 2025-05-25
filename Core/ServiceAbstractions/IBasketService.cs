using Shared.DTO.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractions
{
    public interface IBasketService
    {
        Task<bool> DeleteBasket(string basketId);
        Task<CustomerBasketDto?> GetCustomerBasket(string basketId);
        Task<CustomerBasketDto?> CreateOrUpdateBasket(CustomerBasketDto basketDto);
    }
}
