using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Options;
using QrF.ABP.AspNetCore.Configuration;
using QrF.ABP.Dependency;
using QrF.ABP.Modules;
using QrF.ABP.Reflection.Extensions;
using QrF.ABP.Web;
using QrF.ABP.Web.Configuration.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QrF.ABP.AspNetCore
{
    [DependsOn(typeof(WebModule))]
    public class AspNetCoreModule : BaseModule
    {
        public override void PreInitialize()
        {
            //IocManager.AddConventionalRegistrar(new AspNetCoreConventionalRegistrar());

            //IocManager.Register<IAspNetCoreConfiguration, AspNetCoreConfiguration>();

            //Configuration.ReplaceService<IPrincipalAccessor, AspNetCorePrincipalAccessor>(DependencyLifeStyle.Transient);
            //Configuration.ReplaceService<IAntiForgeryManager, AspNetCoreAntiForgeryManager>(DependencyLifeStyle.Transient);

            //Configuration.Modules.AspNetCore().FormBodyBindingIgnoredTypes.Add(typeof(IFormFile));
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AspNetCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            AddApplicationParts();
            ConfigureAntiforgery();
        }

        private void AddApplicationParts()
        {
            var configuration = IocManager.Resolve<AspNetCoreConfiguration>();
            var partManager = IocManager.Resolve<ApplicationPartManager>();
            var moduleManager = IocManager.Resolve<IModuleManager>();

            var controllerAssemblies = configuration.ControllerAssemblySettings.Select(s => s.Assembly).Distinct();
            foreach (var controllerAssembly in controllerAssemblies)
            {
                partManager.ApplicationParts.Add(new AssemblyPart(controllerAssembly));
            }

            var plugInAssemblies = moduleManager.Modules.Where(m => m.IsLoadedAsPlugIn).Select(m => m.Assembly).Distinct();
            foreach (var plugInAssembly in plugInAssemblies)
            {
                partManager.ApplicationParts.Add(new AssemblyPart(plugInAssembly));
            }
        }

        private void ConfigureAntiforgery()
        {
            IocManager.Using<IOptions<AntiforgeryOptions>>(optionsAccessor =>
            {
                optionsAccessor.Value.HeaderName = Configuration.Modules.AbpWeb().AntiForgery.TokenHeaderName;
            });
        }
    }
}
