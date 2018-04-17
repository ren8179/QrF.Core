using QrF.ABP.Dependency;
using QrF.ABP.Runtime.Caching.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Configuration.Startup
{
    /// <summary>
    /// Used to configure and modules on startup.
    /// </summary>
    public interface IStartupConfiguration : IDictionaryBasedConfig
    {
        /// <summary>
        /// Gets the IOC manager associated with this configuration.
        /// </summary>
        IIocManager IocManager { get; }
        
        /// <summary>
        /// Used to configure <see cref="IEventBus"/>.
        /// </summary>
        IEventBusConfiguration EventBus { get; }
        
        /// <summary>
        /// Used to configure caching.
        /// </summary>
        ICachingConfiguration Caching { get; }
        
        /// <summary>
        /// Used to configure settings.
        /// </summary>
        ISettingsConfiguration Settings { get; }

        /// <summary>
        /// Gets/sets default connection string used by ORM module.
        /// It can be name of a connection string in application's config file or can be full connection string.
        /// </summary>
        string DefaultNameOrConnectionString { get; set; }

        /// <summary>
        /// Used to configure modules.
        /// Modules can write extension methods to <see cref="IModuleConfigurations"/> to add module specific configurations.
        /// </summary>
        IModuleConfigurations Modules { get; }
        
        /// <summary>
        /// Used to replace a service type.
        /// Given <see cref="replaceAction"/> should register an implementation for the <see cref="type"/>.
        /// </summary>
        /// <param name="type">The type to be replaced.</param>
        /// <param name="replaceAction">Replace action.</param>
        void ReplaceService(Type type, Action replaceAction);

        /// <summary>
        /// Gets a configuration object.
        /// </summary>
        T Get<T>();
    }
}
