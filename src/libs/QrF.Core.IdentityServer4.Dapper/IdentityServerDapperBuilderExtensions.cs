using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QrF.Core.IdentityServer4.Dapper.Caches;
using QrF.Core.IdentityServer4.Dapper.HostedServices;
using QrF.Core.IdentityServer4.Dapper.Options;
using QrF.Core.IdentityServer4.Dapper.ResponseHandling;
using QrF.Core.IdentityServer4.Dapper.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.IdentityServer4.Dapper
{
    public static class IdentityServerDapperBuilderExtensions
    {
        /// <summary>
        /// 配置Dapper接口和实现(默认使用SqlServer)
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="storeOptionsAction">存储配置信息</param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddDapperStore(
            this IIdentityServerBuilder builder,
            Action<DapperStoreOptions> storeOptionsAction = null)
        {
            var options = new DapperStoreOptions();
            builder.Services.AddSingleton(options);
            storeOptionsAction?.Invoke(options);
            builder.Services.AddSingleton<TokenCleanup>();
            builder.Services.AddSingleton<IHostedService, TokenCleanupHost>();
            builder.Services.AddSingleton<ITokenResponseGenerator, CusTokenResponseGenerator>();
            builder.Services.AddTransient(typeof(ICache<>), typeof(RedisCache<>));
            builder.Services.AddTransient<IIntrospectionRequestValidator, IntrospectionRequestValidator>();
            return builder;
        }
    }
}
