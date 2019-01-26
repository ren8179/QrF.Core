using Microsoft.Extensions.DependencyInjection;
using QrF.Core.IdentityServer4.Dapper.Options;
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
            //builder.Services.AddTransient<IClientStore, SqlServerClientStore>();
            //builder.Services.AddTransient<IResourceStore, SqlServerResourceStore>();
            //builder.Services.AddTransient<IPersistedGrantStore, SqlServerPersistedGrantStore>();
            //builder.Services.AddTransient<IPersistedGrants, SqlServerPersistedGrants>();
            //builder.Services.AddSingleton<TokenCleanup>();
            //builder.Services.AddSingleton<IHostedService, TokenCleanupHost>();
            //builder.Services.AddSingleton<ITokenResponseGenerator, CzarTokenResponseGenerator>();
            //builder.Services.AddTransient(typeof(ICache<>), typeof(CzarRedisCache<>));
            //builder.Services.AddTransient<IIntrospectionRequestValidator, CzarIntrospectionRequestValidator>();
            return builder;
        }
    }
}
