using DomainLayer.Models.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetCustomerBasket(string id);
        Task<CustomerBasket?> CreateOrUpdateBasket(CustomerBasket customerBasket);
        Task<bool> DeleteBasket(string key);
    }
}
