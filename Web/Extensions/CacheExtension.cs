using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Web.Extensions
{
    public static class CacheExtension
    {
        public static async Task<T> GetCacheValueAsync<T>(this IDistributedCache cache, string key) where T : class
        {
            string result = await cache.GetStringAsync(key);

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            var deserializedObj = JsonConvert.DeserializeObject<T>(result);

            return deserializedObj;
        }

        public static async Task SetCacheValueAsync<T>(
            this IDistributedCache cache,
            string key,
            T value,
            CancellationToken cancellation) where T : class
        {
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions
            {
                // Remove item from cache after duration
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),

                // Remove item from cache if unsued for the duration
                SlidingExpiration = TimeSpan.FromSeconds(30)
            };

            string result = JsonConvert.SerializeObject(value);

            await cache.SetStringAsync(key, result, cacheEntryOptions, cancellation);
        }
    }
}