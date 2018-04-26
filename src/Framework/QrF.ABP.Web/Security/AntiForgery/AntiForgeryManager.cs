using Castle.Core.Logging;
using QrF.ABP.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Web.Security.AntiForgery
{
    public class AntiForgeryManager : IAntiForgeryManager, ITransientDependency
    {
        public ILogger Logger { protected get; set; }

        public IAntiForgeryConfiguration Configuration { get; }

        public AntiForgeryManager(IAntiForgeryConfiguration configuration)
        {
            Configuration = configuration;
            Logger = NullLogger.Instance;
        }

        public virtual string GenerateToken()
        {
            return Guid.NewGuid().ToString("D");
        }
    }
}
