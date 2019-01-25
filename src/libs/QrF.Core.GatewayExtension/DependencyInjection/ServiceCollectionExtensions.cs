using Microsoft.Extensions.DependencyInjection;
using Ocelot.Cache;
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
            //配置文件仓储注入
            //builder.Services.AddSingleton<IFileConfigurationRepository, SqlServerFileConfigurationRepository>();
            //builder.Services.AddSingleton<IClientAuthenticationRepository, SqlServerClientAuthenticationRepository>();
            //builder.Services.AddSingleton<IClientRateLimitRepository, SqlServerClientRateLimitRepository>();
            //builder.Services.AddSingleton<IRpcRepository, SqlServerRpcRepository>();
            //注册后端服务
            builder.Services.AddHostedService<DbConfigurationPoller>();
            //使用Redis重写缓存
            builder.Services.AddSingleton<IOcelotCache<FileConfiguration>, InRedisCache<FileConfiguration>>();
            builder.Services.AddSingleton<IOcelotCache<CachedResponse>, InRedisCache<CachedResponse>>();
            builder.Services.AddSingleton<IInternalConfigurationRepository, RedisInternalConfigurationRepository>();
            builder.Services.AddSingleton<IOcelotCache<ClientRoleModel>, InRedisCache<ClientRoleModel>>();
            builder.Services.AddSingleton<IOcelotCache<RateLimitRuleModel>, InRedisCache<RateLimitRuleModel>>();
            builder.Services.AddSingleton<IOcelotCache<DiffClientRateLimitCounter?>, InRedisCache<DiffClientRateLimitCounter?>>();
            //注入授权
            builder.Services.AddSingleton<ICusAuthenticationProcessor, CusAuthenticationProcessor>();
            //注入限流实现
            builder.Services.AddSingleton<IClientRateLimitProcessor, DiffClientRateLimitProcessor>();

            //重写错误状态码
            builder.Services.AddSingleton<IErrorsToHttpStatusCodeMapper, QrF.Core.GatewayExtension.Responder.ErrorsToHttpStatusCodeMapper>();
            
            return builder;
        }

    }
}
