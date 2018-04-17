using QrF.ABP.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Configuration
{
    /// <summary>
    /// Inherit this class to define settings for a module/application.
    /// </summary>
    public abstract class SettingProvider : ITransientDependency
    {
        /// <summary>
        /// Gets all setting definitions provided by this provider.
        /// </summary>
        /// <returns>List of settings</returns>
        public abstract IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context);
    }
}
