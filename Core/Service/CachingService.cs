using DomainLayer.Contracts;
using ServiceAbstractions;
using System.Text.Json;

namespace Service
{
    public class CachingService(ICachingRepository cachingRepository) : ICachingService
    {
        public async Task<string?> GetAsync(string key)
        {
            return await cachingRepository.GetAsync(key);
        }

        public async Task SetAsync(string key, object value, TimeSpan? expiration = null)
        {
            var cacheValue= JsonSerializer.Serialize(value);
            await cachingRepository.SetAsync(key, cacheValue, expiration);
        }
    }
}
