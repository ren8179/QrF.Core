using Ocelot.Cache;
using QrF.Core.GatewayExtension.Configuration;
using QrF.Core.GatewayExtension.Configuration.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QrF.Core.GatewayExtension.RateLimit
{
    /// <summary>
    /// 实现客户端限流处理器
    /// </summary>
    public class DiffClientRateLimitProcessor : IClientRateLimitProcessor
    {
        private readonly CusOcelotConfiguration _options;
        private readonly IOcelotCache<ClientRoleModel> _ocelotCache;
        private readonly IOcelotCache<RateLimitRuleModel> _rateLimitRuleCache;
        private readonly IOcelotCache<DiffClientRateLimitCounter?> _clientRateLimitCounter;
        private readonly IClientRateLimitRepository _clientRateLimitRepository;
        private static readonly object _processLocker = new object();
        public DiffClientRateLimitProcessor(CusOcelotConfiguration options, 
            IClientRateLimitRepository clientRateLimitRepository, 
            IOcelotCache<DiffClientRateLimitCounter?> clientRateLimitCounter, 
            IOcelotCache<ClientRoleModel> ocelotCache, 
            IOcelotCache<RateLimitRuleModel> rateLimitRuleCache)
        {
            _options = options;
            _clientRateLimitRepository = clientRateLimitRepository;
            _clientRateLimitCounter = clientRateLimitCounter;
            _ocelotCache = ocelotCache;
            _rateLimitRuleCache = rateLimitRuleCache;
        }
        
        /// <summary>
        /// 校验客户端限流结果
        /// </summary>
        /// <param name="clientid">客户端ID</param>
        /// <param name="path">请求地址</param>
        /// <returns></returns>
        public async Task<bool> CheckClientRateLimitResultAsync(string clientid, string path)
        {

            var result = false;
            var clientRule = new List<DiffClientRateLimitOptions>();
            //1、校验路由是否有限流策略
            result = !await CheckReRouteRuleAsync(path);
            if (!result)
            {//2、校验客户端是否被限流了
                var limitResult = await CheckClientRateLimitAsync(clientid, path);
                result = !limitResult.RateLimit;
                clientRule = limitResult.RateLimitOptions;
            }
            if (!result)
            {//3、校验客户端是否启动白名单
                result = await CheckClientReRouteWhiteListAsync(clientid, path);
            }
            if (!result)
            {//4、校验是否触发限流及计数
                result = CheckRateLimitResult(clientRule);
            }
            return result;

        }

        /// <summary>
        /// 检验是否启用限流规则
        /// </summary>
        /// <param name="path">请求地址</param>
        /// <returns></returns>
        private async Task<bool> CheckReRouteRuleAsync(string path)
        {
            var region = _options.RedisOcelotKeyPrefix + "CheckReRouteRuleAsync";
            var key = path;
            var cacheResult = _ocelotCache.Get(key, region);
            if (cacheResult != null)
            {//提取缓存数据
                return cacheResult.Role;
            }
            else
            {//重新获取限流策略
                var result = await _clientRateLimitRepository.CheckReRouteRuleAsync(path);
                _ocelotCache.Add(key, new ClientRoleModel() { CacheTime = DateTime.Now, Role = result }, TimeSpan.FromSeconds(_options.CacheTime), region);
                return result;
            }

        }

        /// <summary>
        /// 校验客户端限流规则
        /// </summary>
        /// <param name="clientid">客户端ID</param>
        /// <param name="path">请求地址</param>
        /// <returns></returns>
        private async Task<(bool RateLimit, List<DiffClientRateLimitOptions> RateLimitOptions)> CheckClientRateLimitAsync(string clientid, string path)
        {
            var region = _options.RedisOcelotKeyPrefix + "CheckClientRateLimitAsync";
            var key = clientid + path;
            var cacheResult = _rateLimitRuleCache.Get(key, region);
            if (cacheResult != null)
            {//提取缓存数据
                return (cacheResult.RateLimit, cacheResult.RateLimitOptions);
            }
            else
            {//重新获取限流策略
                var result = await _clientRateLimitRepository.CheckClientRateLimitAsync(clientid, path);
                _rateLimitRuleCache.Add(key, new RateLimitRuleModel() { RateLimit = result.RateLimit, RateLimitOptions = result.rateLimitOptions }, TimeSpan.FromSeconds(_options.CacheTime), region);
                return result;
            }
        }

        /// <summary>
        /// 校验是否设置了路由白名单
        /// </summary>
        /// <param name="clientid">客户端ID</param>
        /// <param name="path">请求地址</param>
        /// <returns></returns>
        private async Task<bool> CheckClientReRouteWhiteListAsync(string clientid, string path)
        {
            var region = _options.RedisOcelotKeyPrefix + "CheckClientReRouteWhiteListAsync";
            var key = region + clientid + path;
            var cacheResult = _ocelotCache.Get(key, region);
            if (cacheResult != null)
            {//提取缓存数据
                return cacheResult.Role;
            }
            else
            {//重新获取限流策略
                var result = await _clientRateLimitRepository.CheckClientReRouteWhiteListAsync(clientid, path);
                _ocelotCache.Add(key, new ClientRoleModel() { CacheTime = DateTime.Now, Role = result }, TimeSpan.FromSeconds(_options.CacheTime), region);
                return result;
            }
        }

        /// <summary>
        /// 校验完整的限流规则
        /// </summary>
        /// <param name="rateLimitOptions">限流配置</param>
        /// <returns></returns>
        private bool CheckRateLimitResult(List<DiffClientRateLimitOptions> rateLimitOptions)
        {
            bool result = true;
            if (rateLimitOptions != null && rateLimitOptions.Count > 0)
            {//校验策略
                foreach (var op in rateLimitOptions)
                {
                    var counter = new DiffClientRateLimitCounter(DateTime.UtcNow, 1);
                    //分别对每个策略校验
                    var enablePrefix = _options.RedisOcelotKeyPrefix + "RateLimitRule";
                    var key = CusKeyHelper.ComputeCounterKey(enablePrefix, op.ClientId, op.Period, op.RateLimitPath);
                    var periodTimestamp = CusKeyHelper.ConvertToSecond(op.Period);
                    lock (_processLocker)
                    {
                        var rateLimitCounter = _clientRateLimitCounter.Get(key, enablePrefix);
                        if (rateLimitCounter.HasValue)
                        {//提取当前的计数情况
                         // 请求次数增长
                            var totalRequests = rateLimitCounter.Value.TotalRequests + 1;
                            // 深拷贝
                            counter = new DiffClientRateLimitCounter(rateLimitCounter.Value.Timestamp, totalRequests);
                        }
                        else
                        {//写入限流策略
                            _clientRateLimitCounter.Add(key, counter, TimeSpan.FromSeconds(periodTimestamp), enablePrefix);
                        }
                    }
                    if (counter.TotalRequests > op.Limit)
                    {//更新请求记录，并标记为失败
                        result = false;
                    }
                    if (counter.TotalRequests > 1 && counter.TotalRequests <= op.Limit)
                    {//更新缓存配置信息
                        //获取限流剩余时间
                        var cur = (int)(counter.Timestamp.AddSeconds(periodTimestamp) - DateTime.UtcNow).TotalSeconds;
                        _clientRateLimitCounter.Add(key, counter, TimeSpan.FromSeconds(cur), enablePrefix);
                    }
                }
            }
            return result;
        }
    }
}
