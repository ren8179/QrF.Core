using IdentityServer4.Services;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace QrF.Core.IdentityServer4.Dapper.Caches
{
    public class MemoryCache<T> : ICache<T> where T : class
    {
        private const string KeySeparator = ":";
        private readonly IMemoryCache _cache;
        public MemoryCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        private string GetKey(string key)
        {
            return typeof(T).FullName + KeySeparator + key;
        }

        public async Task<T> GetAsync(string key)
        {
            key = GetKey(key);
            var result =await Task.Run(() => _cache.Get<T>(key));
            return result;
        }

        public async Task SetAsync(string key, T item, TimeSpan expiration)
        {
            key = GetKey(key);
            await Task.Run(() => _cache.Set(key, item, expiration));
        }
    }
}
