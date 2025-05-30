using DomainLayer.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICachingRepository
    {
        readonly IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            var cachValue = await _database.StringGetAsync(key);
            return cachValue.IsNull ? null : cachValue.ToString();
        }

        public async Task SetAsync(string key, string value, TimeSpan? expiration = null)
        {
            await _database.StringSetAsync(key, JsonSerializer.Serialize(value), expiration);
        }
    }
}
