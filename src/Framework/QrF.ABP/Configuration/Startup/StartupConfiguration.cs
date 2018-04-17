using QrF.ABP.Dependency;
using QrF.ABP.Runtime.Caching.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Configuration.Startup
{
    /// <summary>
    /// This class is used to configure modules on startup.
    /// </summary>
    internal class StartupConfiguration : DictionaryBasedConfig, IStartupConfiguration
    {
        /// <summary>
        /// Reference to the IocManager.
        /// </summary>
        public IIocManager IocManager { get; }
        
        /// <summary>
        /// Used to configure settings.
        /// </summary>
        public ISettingsConfiguration Settings { get; private set; }

        /// <summary>
        /// Gets/sets default connection string used by ORM module.
        /// It can be name of a connection string in application's config file or can be full connection string.
        /// </summary>
        public string DefaultNameOrConnectionString { get; set; }

        /// <summary>
        /// Used to configure modules.
        /// Modules can write extension methods to <see cref="ModuleConfigurations"/> to add module specific configurations.
        /// </summary>
        public IModuleConfigurations Modules { get; private set; }
        
        /// <summary>
        /// Used to configure <see cref="IEventBus"/>.
        /// </summary>
        public IEventBusConfiguration EventBus { get; private set; }
        
        public ICachingConfiguration Caching { get; private set; }
        
        public Dictionary<Type, Action> ServiceReplaceActions { get; private set; }
        
        /// <summary>
        /// Private constructor for singleton pattern.
        /// </summary>
        public StartupConfiguration(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        public void Initialize()
        {
            Modules = IocManager.Resolve<IModuleConfigurations>();
            Settings = IocManager.Resolve<ISettingsConfiguration>();
            EventBus = IocManager.Resolve<IEventBusConfiguration>();
            Caching = IocManager.Resolve<ICachingConfiguration>();

            ServiceReplaceActions = new Dictionary<Type, Action>();
        }

        public void ReplaceService(Type type, Action replaceAction)
        {
            ServiceReplaceActions[type] = replaceAction;
        }

        public T Get<T>()
        {
            return GetOrCreate(typeof(T).FullName, () => IocManager.Resolve<T>());
        }
    }
}
