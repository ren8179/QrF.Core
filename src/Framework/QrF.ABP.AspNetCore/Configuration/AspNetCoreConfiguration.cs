using Microsoft.AspNetCore.Routing;
using QrF.ABP.Web.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace QrF.ABP.AspNetCore.Configuration
{
    public class AspNetCoreConfiguration : IAspNetCoreConfiguration
    {
        public WrapResultAttribute DefaultWrapResultAttribute { get; }

        public List<Type> FormBodyBindingIgnoredTypes { get; }

        public ControllerAssemblySettingList ControllerAssemblySettings { get; }

        public bool IsValidationEnabledForControllers { get; set; }

        public bool IsAuditingEnabled { get; set; }

        public bool SetNoCacheForAjaxResponses { get; set; }

        public List<Action<IRouteBuilder>> RouteConfiguration { get; }

        public AspNetCoreConfiguration()
        {
            DefaultWrapResultAttribute = new WrapResultAttribute();
            ControllerAssemblySettings = new ControllerAssemblySettingList();
            FormBodyBindingIgnoredTypes = new List<Type>();
            RouteConfiguration = new List<Action<IRouteBuilder>>();
            IsValidationEnabledForControllers = true;
            SetNoCacheForAjaxResponses = true;
            IsAuditingEnabled = true;
        }

        public ControllerAssemblySettingBuilder CreateControllersForAppServices(
            Assembly assembly,
            string moduleName = ControllerAssemblySetting.DefaultServiceModuleName,
            bool useConventionalHttpVerbs = true)
        {
            var setting = new ControllerAssemblySetting(moduleName, assembly, useConventionalHttpVerbs);
            ControllerAssemblySettings.Add(setting);
            return new ControllerAssemblySettingBuilder(setting);
        }
    }
}
