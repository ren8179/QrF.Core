using QrF.ABP.Configuration.Startup;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.AspNetCore.Configuration
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure ABP ASP.NET Core module.
    /// </summary>
    public static class AspNetCoreConfigurationExtensions
    {
        /// <summary>
        /// Used to configure ABP ASP.NET Core module.
        /// </summary>
        public static IAspNetCoreConfiguration AbpAspNetCore(this IModuleConfigurations configurations)
        {
            return configurations.Configuration.Get<IAspNetCoreConfiguration>();
        }
    }
}
