using Microsoft.Extensions.Caching.Memory;
using Ocelot.Cache;
using QrF.Core.GatewayExtension.Configuration;
using QrF.Core.GatewayExtension.RateLimit;
using QrF.Core.Utils.Extension;
using System;

namespace QrF.Core.GatewayExtension.Cache
{
    public class MemoryCache<T> : IOcelotCache<T>
    {
        private readonly CusOcelotConfiguration _options;
        private readonly IMemoryCache _cache;
        public MemoryCache(CusOcelotConfiguration options, IMemoryCache cache)
        {
            _options = options;
            _cache = cache;
        }
        public void Add(string key, T value, TimeSpan ttl, string region)
        {
            key = CusKeyHelper.GetKey(_options.RedisOcelotKeyPrefix, region, key);
            if (_options.ClusterEnvironment)
            {
                var msg = value.ToJson();
                if (typeof(T) == typeof(CachedResponse))
                {//带过期时间的缓存
                    _cache.Set(key, value, ttl); //添加本地缓存
                    RedisHelper.Set(key, msg); //加入redis缓存
                    RedisHelper.Publish(key, msg); //发布
                }
                else if (typeof(T) == typeof(DiffClientRateLimitCounter?))
                {//限流缓存，直接使用redis
                    RedisHelper.Set(key, value, (int)ttl.TotalSeconds);
                }
                else
                {//正常缓存,发布
                    _cache.Set(key, value, ttl); //添加本地缓存
                    RedisHelper.Set(key, msg); //加入redis缓存
                    RedisHelper.Publish(key, msg); //发布
                }
            }
            else
            {
                _cache.Set(key, value, ttl); //添加本地缓存
            }
        }
        public void AddAndDelete(string key, T value, TimeSpan ttl, string region)
        {
            Add(key, value, ttl, region);
        }

        public void ClearRegion(string region)
        {
            if (_options.ClusterEnvironment)
            {
                var keys = RedisHelper.Keys(region + "*");
                RedisHelper.Del(keys);
                foreach (var key in keys)
                {
                    RedisHelper.Publish(key, ""); //发布key值为空，处理时删除即可。
                }
            }
            else
            {
                _cache.Remove(region);
            }
        }

        public T Get(string key, string region)
        {
            key = CusKeyHelper.GetKey(_options.RedisOcelotKeyPrefix, region, key);
            if (region == nameof(DiffClientRateLimitCounter) && _options.ClusterEnvironment)
            {//限流且开启了集群支持，默认从redis取
                return RedisHelper.Get<T>(key);
            }
            var result = _cache.Get<T>(key);
            if (result == null && _options.ClusterEnvironment)
            {
                result = RedisHelper.Get<T>(key);
                if (result != null)
                {
                    if (typeof(T) == typeof(CachedResponse))
                    {//查看redis过期时间
                        var second = RedisHelper.Ttl(key);
                        if (second > 0)
                        {
                            _cache.Set(key, result, TimeSpan.FromSeconds(second));
                        }
                    }
                    else
                    {
                        _cache.Set(key, result, TimeSpan.FromSeconds(_options.CacheTime));
                    }
                }
            }
            return result;
        }
    }
}
