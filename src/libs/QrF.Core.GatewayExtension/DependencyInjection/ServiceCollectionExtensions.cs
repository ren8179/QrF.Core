using Microsoft.Extensions.DependencyInjection;
using Ocelot.Cache;
using Ocelot.Configuration;
using Ocelot.Configuration.File;
using Ocelot.Configuration.Repository;
using Ocelot.DependencyInjection;
using Ocelot.Responder;
using QrF.Core.GatewayExtension.Authentication;
using QrF.Core.GatewayExtension.Cache;
using QrF.Core.GatewayExtension.Configuration;
using QrF.Core.GatewayExtension.Configuration.Model;
using QrF.Core.GatewayExtension.RateLimit;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.GatewayExtension.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加默认的注入方式，所有需要传入的参数都是用默认值
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IOcelotBuilder AddExtOcelot(this IOcelotBuilder builder, Action<CusOcelotConfiguration> option)
        {
            var options = new CusOcelotConfiguration();
            builder.Services.AddSingleton(options);
            option?.Invoke(options);
            //注册后端服务
            builder.Services.AddHostedService<DbConfigurationPoller>();
            builder.Services.AddMemoryCache(); //添加本地缓存
            #region 启动Redis缓存，并支持普通模式 官方集群模式  哨兵模式 分区模式
            if (options.ClusterEnvironment)
            {
                //默认使用普通模式
                var csredis = new CSRedis.CSRedisClient(options.RedisConnectionString);
                switch (options.RedisStoreMode)
                {
                    case RedisStoreMode.Partition:
                        var NodesIndex = options.RedisSentinelOrPartitionConStr;
                        Func<string, string> nodeRule = null;
                        csredis = new CSRedis.CSRedisClient(nodeRule, options.RedisSentinelOrPartitionConStr);
                        break;
                    case RedisStoreMode.Sentinel:
                        csredis = new CSRedis.CSRedisClient(options.RedisConnectionString, options.RedisSentinelOrPartitionConStr);
                        break;
                }
                //初始化 RedisHelper
                RedisHelper.Initialization(csredis);
            }
            #endregion
            //重写缓存
            builder.Services.AddSingleton<IOcelotCache<FileConfiguration>, MemoryCache<FileConfiguration>>();
            builder.Services.AddSingleton<IOcelotCache<InternalConfiguration>, MemoryCache<InternalConfiguration>>();
            builder.Services.AddSingleton<IOcelotCache<CachedResponse>, MemoryCache<CachedResponse>>();
            builder.Services.AddSingleton<IInternalConfigurationRepository, RedisInternalConfigurationRepository>();
            builder.Services.AddSingleton<IOcelotCache<ClientRoleModel>, MemoryCache<ClientRoleModel>>();
            builder.Services.AddSingleton<IOcelotCache<RateLimitRuleModel>, MemoryCache<RateLimitRuleModel>>();
            builder.Services.AddSingleton<IOcelotCache<DiffClientRateLimitCounter?>, MemoryCache<DiffClientRateLimitCounter?>>();
            //注入授权
            builder.Services.AddSingleton<ICusAuthenticationProcessor, CusAuthenticationProcessor>();
            //注入限流实现
            builder.Services.AddSingleton<IClientRateLimitProcessor, DiffClientRateLimitProcessor>();

            //重写错误状态码
            builder.Services.AddSingleton<IErrorsToHttpStatusCodeMapper, QrF.Core.GatewayExtension.Responder.ErrorsToHttpStatusCodeMapper>();

            //http输出转换类
            builder.Services.AddSingleton<IHttpResponder, HttpContextResponder>();

            return builder;
        }

    }
}
