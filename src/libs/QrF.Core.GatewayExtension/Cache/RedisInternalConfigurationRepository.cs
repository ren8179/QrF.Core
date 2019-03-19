using Ocelot.Cache;
using Ocelot.Configuration;
using Ocelot.Configuration.Creator;
using Ocelot.Configuration.Repository;
using Ocelot.Responses;
using QrF.Core.GatewayExtension.Configuration;
using QrF.Core.Utils.Extension;
using System;

namespace QrF.Core.GatewayExtension.Cache
{
    public class RedisInternalConfigurationRepository : IInternalConfigurationRepository
    {
        private readonly CusOcelotConfiguration _options;
        private IFileConfigurationRepository _fileConfigurationRepository;
        private IInternalConfigurationCreator _internalConfigurationCreator;
        private readonly IOcelotCache<InternalConfiguration> _ocelotCache;
        public RedisInternalConfigurationRepository(CusOcelotConfiguration options, IFileConfigurationRepository fileConfigurationRepository, IInternalConfigurationCreator internalConfigurationCreator, IOcelotCache<InternalConfiguration> ocelotCache)
        {
            _fileConfigurationRepository = fileConfigurationRepository;
            _internalConfigurationCreator = internalConfigurationCreator;
            _options = options;
            _ocelotCache = ocelotCache;
        }

        /// <summary>
        /// 设置配置信息
        /// </summary>
        /// <param name="internalConfiguration">配置信息</param>
        /// <returns></returns>
        public Response AddOrReplace(IInternalConfiguration internalConfiguration)
        {
            var key = _options.RedisOcelotKeyPrefix + nameof(InternalConfiguration);
            _ocelotCache.Add(key, (InternalConfiguration)internalConfiguration, TimeSpan.FromSeconds(_options.CacheTime), "");
            return new OkResponse();
        }

        /// <summary>
        /// 从缓存中获取配置信息
        /// </summary>
        /// <returns></returns>
        public Response<IInternalConfiguration> Get()
        {
            var key = _options.RedisOcelotKeyPrefix + nameof(InternalConfiguration);
            var result = _ocelotCache.Get(key, "");
            if (result != null)
            {
                return new OkResponse<IInternalConfiguration>(result);
            }
            var fileconfig = _fileConfigurationRepository.Get().Result;
            var internalConfig = _internalConfigurationCreator.Create(fileconfig.Data).Result;
            AddOrReplace(internalConfig.Data);
            return new OkResponse<IInternalConfiguration>(internalConfig.Data);
        }
    }
}
