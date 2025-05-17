using DomainLayer.Contracts;
using DomainLayer.Models.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        public Task<CustomerBasket?> CreateOrUpdateBasket(CustomerBasket customerBasket)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBasket(string key)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerBasket> GetCustomerBasket(string id)
        {
            throw new NotImplementedException();
        }
    }
}
