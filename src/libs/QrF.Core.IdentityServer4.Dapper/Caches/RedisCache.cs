using IdentityServer4.Services;
using QrF.Core.IdentityServer4.Dapper.Options;
using System;
using System.Threading.Tasks;

namespace QrF.Core.IdentityServer4.Dapper.Caches
{
    public class RedisCache<T> : ICache<T>
        where T : class
    {
        private const string KeySeparator = ":";
        public RedisCache(DapperStoreOptions configurationStoreOptions)
        {
            CSRedis.CSRedisClient csredis;
            if (configurationStoreOptions.RedisConnectionStrings.Count == 1)
            {
                //普通模式
                csredis = new CSRedis.CSRedisClient(configurationStoreOptions.RedisConnectionStrings[0]);
            }
            else
            {
                //集群模式
                //实现思路：根据key.GetHashCode() % 节点总数量，确定连向的节点
                //也可以自定义规则(第一个参数设置)
                csredis = new CSRedis.CSRedisClient(item => item, configurationStoreOptions.RedisConnectionStrings.ToArray());
            }
            //初始化 RedisHelper
            RedisHelper.Initialization(csredis);
        }

        private string GetKey(string key)
        {
            return typeof(T).FullName + KeySeparator + key;
        }

        public async Task<T> GetAsync(string key)
        {
            key = GetKey(key);
            var result = await RedisHelper.GetAsync<T>(key);
            return result;
        }

        public async Task SetAsync(string key, T item, TimeSpan expiration)
        {
            key = GetKey(key);
            await RedisHelper.SetAsync(key, item, (int)expiration.TotalSeconds);
        }
    }
}
