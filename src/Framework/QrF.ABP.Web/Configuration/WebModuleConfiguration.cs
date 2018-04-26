using QrF.ABP.Web.Api.ProxyScripting.Configuration;
using QrF.ABP.Web.Security.AntiForgery;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Web.Configuration
{
    public class WebModuleConfiguration : IWebModuleConfiguration
    {
        public bool SendAllExceptionsToClients { get; set; }

        public IApiProxyScriptingConfiguration ApiProxyScripting { get; }

        public IAntiForgeryConfiguration AntiForgery { get; }

        public WebModuleConfiguration(
            IApiProxyScriptingConfiguration apiProxyScripting,
            IAntiForgeryConfiguration antiForgery)
        {
            ApiProxyScripting = apiProxyScripting;
            AntiForgery = antiForgery;
        }
    }
}
