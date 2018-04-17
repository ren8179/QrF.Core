using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Runtime.Caching.Memory
{
    /// <summary>
    /// Implements <see cref="ICache"/> to work with <see cref="Microsoft.Extensions.Caching.Memory.MemoryCache"/>.
    /// </summary>
    public class MemoryCache : CacheBase
    {
        private Microsoft.Extensions.Caching.Memory.MemoryCache _memoryCache;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Unique name of the cache</param>
        public MemoryCache(string name)
            : base(name)
        {
            _memoryCache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new OptionsWrapper<MemoryCacheOptions>(new MemoryCacheOptions()));
        }

        public override object GetOrDefault(string key)
        {
            return _memoryCache.Get(key);
        }

        public override void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            if (value == null)
            {
                throw new BaseException("Can not insert null values to the cache!");
            }

            if (absoluteExpireTime != null)
            {
                _memoryCache.Set(key, value, DateTimeOffset.Now.Add(absoluteExpireTime.Value));
            }
            else if (slidingExpireTime != null)
            {
                _memoryCache.Set(key, value, slidingExpireTime.Value);
            }
            else if (DefaultAbsoluteExpireTime != null)
            {
                _memoryCache.Set(key, value, DateTimeOffset.Now.Add(DefaultAbsoluteExpireTime.Value));
            }
            else
            {
                _memoryCache.Set(key, value, DefaultSlidingExpireTime);
            }
        }

        public override void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public override void Clear()
        {
            _memoryCache.Dispose();
            _memoryCache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new OptionsWrapper<MemoryCacheOptions>(new MemoryCacheOptions()));
        }

        public override void Dispose()
        {
            _memoryCache.Dispose();
            base.Dispose();
        }
    }
}
