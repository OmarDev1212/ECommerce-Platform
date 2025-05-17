using DomainLayer.Contracts;
using DomainLayer.Models.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _db = connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasket(CustomerBasket customerBasket, TimeSpan? TimeToLive = null)
        {
            var flag = await _db.StringSetAsync(customerBasket.Id, JsonSerializer.Serialize(customerBasket.Items), TimeToLive);
            return flag ? await GetCustomerBasket(customerBasket.Id) : null;
        }

        public async Task<bool> DeleteBasket(string key) => await _db.KeyDeleteAsync(key);

        public async Task<CustomerBasket?> GetCustomerBasket(string id)
        {
            var basket = await _db.StringGetAsync(id);
            if (basket.IsNull) return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(basket); // as it is key value pair 
        }
    }
}
