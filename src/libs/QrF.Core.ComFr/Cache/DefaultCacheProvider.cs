using CacheManager.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.ComFr.Cache
{
    public class DefaultCacheProvider : ICacheProvider
    {
        private readonly ConcurrentDictionary<string, object> _allCacheManager;
        public DefaultCacheProvider()
        {
            _allCacheManager = new ConcurrentDictionary<string, object>();
        }
        public ICacheManager<T> Build<T>()
        {
            return (ICacheManager<T>)_allCacheManager.GetOrAdd(typeof(T).FullName, k =>
            {
                return new DefaultCacheManager<T>(CacheFactory.Build<T>(setting =>
                {
                    setting.WithDictionaryHandle(k);
                }));
            });
        }
    }
}
