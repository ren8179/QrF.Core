﻿using Ocelot.Configuration;
using Ocelot.Configuration.Creator;
using Ocelot.Configuration.Repository;
using Ocelot.Responses;
using QrF.Core.GatewayExtension.Configuration;
using QrF.Core.Utils.Extension;

namespace QrF.Core.GatewayExtension.Cache
{
    public class RedisInternalConfigurationRepository : IInternalConfigurationRepository
    {
        private readonly CusOcelotConfiguration _options;
        private IFileConfigurationRepository _fileConfigurationRepository;
        private IInternalConfigurationCreator _internalConfigurationCreator;
        public RedisInternalConfigurationRepository(CusOcelotConfiguration options, IFileConfigurationRepository fileConfigurationRepository, IInternalConfigurationCreator internalConfigurationCreator)
        {
            _fileConfigurationRepository = fileConfigurationRepository;
            _internalConfigurationCreator = internalConfigurationCreator;
            _options = options;
            CSRedis.CSRedisClient csredis;
            if (options.RedisConnectionStrings.Count == 1)
            {
                //普通模式
                csredis = new CSRedis.CSRedisClient(options.RedisConnectionStrings[0]);
            }
            else
            {
                //集群模式
                //实现思路：根据key.GetHashCode() % 节点总数量，确定连向的节点
                //也可以自定义规则(第一个参数设置)
                csredis = new CSRedis.CSRedisClient(null, options.RedisConnectionStrings.ToArray());
            }
            //初始化 RedisHelper
            RedisHelper.Initialization(csredis);
        }

        /// <summary>
        /// 设置配置信息
        /// </summary>
        /// <param name="internalConfiguration">配置信息</param>
        /// <returns></returns>
        public Response AddOrReplace(IInternalConfiguration internalConfiguration)
        {
            var key = _options.RedisKeyPrefix + "-internalConfiguration";
            RedisHelper.Set(key, internalConfiguration.ToJson());
            return new OkResponse();
        }

        /// <summary>
        /// 从缓存中获取配置信息
        /// </summary>
        /// <returns></returns>
        public Response<IInternalConfiguration> Get()
        {
            var key = _options.RedisKeyPrefix + "-internalConfiguration";
            var result = RedisHelper.Get<InternalConfiguration>(key);
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
