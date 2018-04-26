using QrF.ABP.Modules;
using QrF.ABP.Web.Api.ProxyScripting.Configuration;
using QrF.ABP.Web.Api.ProxyScripting.Generators.JQuery;
using QrF.ABP.Web.Configuration;
using QrF.ABP.Web.Configuration.Startup;
using QrF.ABP.Web.Security.AntiForgery;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace QrF.ABP.Web
{
    /// <summary>
    /// This module is used to use ABP in ASP.NET web applications.
    /// </summary>
    [DependsOn(typeof(KernelModule))]
    public class WebModule : BaseModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            IocManager.Register<IApiProxyScriptingConfiguration, ApiProxyScriptingConfiguration>();
            IocManager.Register<IAntiForgeryConfiguration, AntiForgeryConfiguration>();
            IocManager.Register<IWebModuleConfiguration, WebModuleConfiguration>();

            Configuration.Modules.AbpWeb().ApiProxyScripting.Generators[JQueryProxyScriptGenerator.Name] = typeof(JQueryProxyScriptGenerator);

        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

    }
}
