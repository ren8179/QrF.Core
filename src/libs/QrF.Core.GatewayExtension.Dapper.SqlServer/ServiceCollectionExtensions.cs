using Microsoft.Extensions.DependencyInjection;
using Ocelot.Configuration.Repository;
using Ocelot.DependencyInjection;
using QrF.Core.GatewayExtension.Authentication;
using QrF.Core.GatewayExtension.Dapper.SqlServer.Stores;
using QrF.Core.GatewayExtension.RateLimit;

namespace QrF.Core.GatewayExtension.Dapper.SqlServer
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static IOcelotBuilder UseSqlServer(this IOcelotBuilder builder)
        {
            builder.Services.AddSingleton<IFileConfigurationRepository, FileConfigurationRepository>();
            builder.Services.AddSingleton<IClientAuthenticationRepository, ClientAuthenticationRepository>();
            builder.Services.AddSingleton<IClientRateLimitRepository, ClientRateLimitRepository>();
            return builder;
        }
    }
}
