using System;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;
using QrF.Core.IdentityServer4.Dapper.Interfaces;

namespace QrF.Core.IdentityServer4.Dapper.SqlServer
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static IIdentityServerBuilder UseSqlServer(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IClientStore, Stores.ClientStore>();
            builder.Services.AddTransient<IResourceStore, Stores.ResourceStore>();
            builder.Services.AddTransient<IPersistedGrantStore, Stores.PersistedGrantStore>();
            builder.Services.AddTransient<IPersistedGrants, Stores.PersistedGrants>();
            return builder;
        }
    }
}
