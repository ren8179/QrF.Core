using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Configuration
{
    /// <summary>
    /// The context that is used in setting providers.
    /// </summary>
    public class SettingDefinitionProviderContext
    {
        public ISettingDefinitionManager Manager { get; }

        internal SettingDefinitionProviderContext(ISettingDefinitionManager manager)
        {
            Manager = manager;
        }
    }
}
