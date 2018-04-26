using QrF.ABP.Configuration.Startup;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.EntityFrameworkCore.Configuration
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure ABP EntityFramework Core module.
    /// </summary>
    public static class EfCoreConfigurationExtensions
    {
        /// <summary>
        /// Used to configure ABP EntityFramework Core module.
        /// </summary>
        public static IEfCoreConfiguration AbpEfCore(this IModuleConfigurations configurations)
        {
            return configurations.Configuration.Get<IEfCoreConfiguration>();
        }
    }
}
