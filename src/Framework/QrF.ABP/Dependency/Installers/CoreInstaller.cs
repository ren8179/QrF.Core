using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using QrF.ABP.Configuration.Startup;
using QrF.ABP.Modules;
using QrF.ABP.PlugIns;
using QrF.ABP.Reflection;
using QrF.ABP.Runtime.Caching.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Dependency.Installers
{
    internal class CoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(

                Component.For<ISettingsConfiguration, SettingsConfiguration>().ImplementedBy<SettingsConfiguration>().LifestyleSingleton(),
                Component.For<IModuleConfigurations, ModuleConfigurations>().ImplementedBy<ModuleConfigurations>().LifestyleSingleton(),
                Component.For<IEventBusConfiguration, EventBusConfiguration>().ImplementedBy<EventBusConfiguration>().LifestyleSingleton(),
                
                Component.For<ICachingConfiguration, CachingConfiguration>().ImplementedBy<CachingConfiguration>().LifestyleSingleton(),
                
                Component.For<IStartupConfiguration, StartupConfiguration>().ImplementedBy<StartupConfiguration>().LifestyleSingleton(),
               
                Component.For<ITypeFinder, TypeFinder>().ImplementedBy<TypeFinder>().LifestyleSingleton(),
                Component.For<IPlugInManager, PlugInManager>().ImplementedBy<PlugInManager>().LifestyleSingleton(),
                Component.For<IModuleManager, ModuleManager>().ImplementedBy<ModuleManager>().LifestyleSingleton(),
                Component.For<IAssemblyFinder, AssemblyFinder>().ImplementedBy<AssemblyFinder>().LifestyleSingleton()
                );
        }
    }
}
