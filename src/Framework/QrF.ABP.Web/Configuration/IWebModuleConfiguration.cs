using QrF.ABP.Web.Api.ProxyScripting.Configuration;
using QrF.ABP.Web.Security.AntiForgery;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Web.Configuration
{
    public interface IWebModuleConfiguration
    {
        /// <summary>
        /// If this is set to true, all exception and details are sent directly to clients on an error.
        /// Default: false (ABP hides exception details from clients except special exceptions.)
        /// </summary>
        bool SendAllExceptionsToClients { get; set; }

        /// <summary>
        /// Used to configure Api proxy scripting.
        /// </summary>
        IApiProxyScriptingConfiguration ApiProxyScripting { get; }

        /// <summary>
        /// Used to configure Anti Forgery security settings.
        /// </summary>
        IAntiForgeryConfiguration AntiForgery { get; }
    }
}
