using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Configuration.Startup
{
    /// <summary>
    /// Used to provide a way to configure modules.
    /// Create entension methods to this class to be used over <see cref="IStartupConfiguration.Modules"/> object.
    /// </summary>
    public interface IModuleConfigurations
    {
        /// <summary>
        /// Gets the configuration object.
        /// </summary>
        IStartupConfiguration Configuration { get; }
    }
}
