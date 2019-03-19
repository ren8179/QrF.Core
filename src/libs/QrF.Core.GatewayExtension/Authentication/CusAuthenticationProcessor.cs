using Ocelot.Cache;
using QrF.Core.GatewayExtension.Configuration;
using QrF.Core.GatewayExtension.Configuration.Model;
using System;
using System.Threading.Tasks;

namespace QrF.Core.GatewayExtension.Authentication
{
    /// <summary>
    /// 实现自定义授权处理器逻辑
    /// </summary>
    public class CusAuthenticationProcessor : ICusAuthenticationProcessor
    {
        private readonly IClientAuthenticationRepository _clientAuthenticationRepository;
        private readonly CusOcelotConfiguration _options;
        private readonly IOcelotCache<ClientRoleModel> _ocelotCache;
        public CusAuthenticationProcessor(IClientAuthenticationRepository clientAuthenticationRepository,
            CusOcelotConfiguration options,
            IOcelotCache<ClientRoleModel> ocelotCache)
        {
            _clientAuthenticationRepository = clientAuthenticationRepository;
            _options = options;
            _ocelotCache = ocelotCache;
        }
        /// <summary>
        /// 校验当前的请求地址客户端是否有权限访问
        /// </summary>
        /// <param name="clientid">客户端ID</param>
        /// <param name="path">请求地址</param>
        /// <returns></returns>
        public async Task<bool> CheckClientAuthenticationAsync(string clientid, string path)
        {
            var enablePrefix = _options.RedisOcelotKeyPrefix + "ClientAuthentication";
            var key = CusKeyHelper.ComputeCounterKey(enablePrefix, clientid, "", path);
            var cacheResult = _ocelotCache.Get(key, enablePrefix);
            if (cacheResult != null)
            {//提取缓存数据
                return cacheResult.Role;
            }
            else
            {//重新获取认证信息
                var result = await _clientAuthenticationRepository.ClientAuthenticationAsync(clientid, path);
                //添加到缓存里
                _ocelotCache.Add(key, new ClientRoleModel() { CacheTime = DateTime.Now, Role = result }, TimeSpan.FromMinutes(_options.CacheTime), enablePrefix);
                return result;
            }
        }
    }
}
