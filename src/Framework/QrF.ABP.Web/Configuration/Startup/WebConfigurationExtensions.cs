using QrF.ABP.Configuration.Startup;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Web.Configuration.Startup
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure ABP Web module.
    /// </summary>
    public static class WebConfigurationExtensions
    {
        /// <summary>
        /// Used to configure ABP Web Common module.
        /// </summary>
        public static IWebModuleConfiguration AbpWeb(this IModuleConfigurations configurations)
        {
            return configurations.Configuration.Get<IWebModuleConfiguration>();
        }
    }
}
