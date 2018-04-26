using Microsoft.AspNetCore.Routing;
using QrF.ABP.Web.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace QrF.ABP.AspNetCore.Configuration
{
    public interface IAspNetCoreConfiguration
    {
        WrapResultAttribute DefaultWrapResultAttribute { get; }
        
        List<Type> FormBodyBindingIgnoredTypes { get; }

        /// <summary>
        /// Default: true.
        /// </summary>
        bool IsValidationEnabledForControllers { get; set; }
        
        /// <summary>
        /// Default: true.
        /// </summary>
        bool SetNoCacheForAjaxResponses { get; set; }

        /// <summary>
        /// Used to add route config for modules.
        /// </summary>
        List<Action<IRouteBuilder>> RouteConfiguration { get; }

        ControllerAssemblySettingBuilder CreateControllersForAppServices(
            Assembly assembly,
            string moduleName = ControllerAssemblySetting.DefaultServiceModuleName,
            bool useConventionalHttpVerbs = true
        );
    }
}
