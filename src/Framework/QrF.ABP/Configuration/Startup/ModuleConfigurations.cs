using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Configuration.Startup
{
    internal class ModuleConfigurations : IModuleConfigurations
    {
        public IStartupConfiguration Configuration { get; private set; }

        public ModuleConfigurations(IStartupConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
