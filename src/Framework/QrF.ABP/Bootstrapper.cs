using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using QrF.ABP.Configuration.Startup;
using QrF.ABP.Dependency;
using QrF.ABP.Dependency.Installers;
using QrF.ABP.Modules;
using QrF.ABP.PlugIns;
using System;
using System.Reflection;

namespace QrF.ABP
{
    /// <summary>
    /// This is the main class that is responsible to start entire ABP system.
    /// Prepares dependency injection and registers core components needed for startup.
    /// It must be instantiated and initialized (see <see cref="Initialize"/>) first in an application.
    /// </summary>
    public class Bootstrapper : IDisposable
    {
        /// <summary>
        /// Get the startup module of the application which depends on other used modules.
        /// </summary>
        public Type StartupModule { get; }

        /// <summary>
        /// A list of plug in folders.
        /// </summary>
        public PlugInSourceList PlugInSources { get; }

        /// <summary>
        /// Gets IIocManager object used by this class.
        /// </summary>
        public IIocManager IocManager { get; }

        /// <summary>
        /// Is this object disposed before?
        /// </summary>
        protected bool IsDisposed;

        private ModuleManager _moduleManager;
        private ILogger _logger;

        /// <summary>
        /// Creates a new <see cref="Bootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="AbpModule"/>.</param>
        /// <param name="optionsAction">An action to set options</param>
        private Bootstrapper(Type startupModule, Action<BootstrapperOptions> optionsAction = null)
        {
            var options = new BootstrapperOptions();
            optionsAction?.Invoke(options);

            if (!typeof(BaseModule).GetTypeInfo().IsAssignableFrom(startupModule))
            {
                throw new ArgumentException($"{nameof(startupModule)} should be derived from {nameof(BaseModule)}.");
            }

            StartupModule = startupModule;

            IocManager = options.IocManager;
            PlugInSources = options.PlugInSources;

            _logger = NullLogger.Instance;

            if (!options.DisableAllInterceptors)
            {
                AddInterceptorRegistrars();
            }
        }

        /// <summary>
        /// Creates a new <see cref="AbpBootstrapper"/> instance.
        /// </summary>
        /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="AbpModule"/>.</typeparam>
        /// <param name="optionsAction">An action to set options</param>
        public static Bootstrapper Create<TStartupModule>(Action<BootstrapperOptions> optionsAction = null)
            where TStartupModule : BaseModule
        {
            return new Bootstrapper(typeof(TStartupModule), optionsAction);
        }

        /// <summary>
        /// Creates a new <see cref="AbpBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="AbpModule"/>.</param>
        /// <param name="optionsAction">An action to set options</param>
        public static Bootstrapper Create(Type startupModule, Action<BootstrapperOptions> optionsAction = null)
        {
            return new Bootstrapper(startupModule, optionsAction);
        }

        /// <summary>
        /// Creates a new <see cref="AbpBootstrapper"/> instance.
        /// </summary>
        /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="AbpModule"/>.</typeparam>
        /// <param name="iocManager">IIocManager that is used to bootstrap the ABP system</param>
        [Obsolete("Use overload with parameter type: Action<AbpBootstrapperOptions> optionsAction")]
        public static Bootstrapper Create<TStartupModule>(IIocManager iocManager)
            where TStartupModule : BaseModule
        {
            return new Bootstrapper(typeof(TStartupModule), options =>
            {
                options.IocManager = iocManager;
            });
        }

        /// <summary>
        /// Creates a new <see cref="AbpBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="AbpModule"/>.</param>
        /// <param name="iocManager">IIocManager that is used to bootstrap the ABP system</param>
        [Obsolete("Use overload with parameter type: Action<AbpBootstrapperOptions> optionsAction")]
        public static Bootstrapper Create(Type startupModule, IIocManager iocManager)
        {
            return new Bootstrapper(startupModule, options =>
            {
                options.IocManager = iocManager;
            });
        }

        private void AddInterceptorRegistrars()
        {

        }

        /// <summary>
        /// Initializes the ABP system.
        /// </summary>
        public virtual void Initialize()
        {
            ResolveLogger();

            try
            {
                RegisterBootstrapper();
                IocManager.IocContainer.Install(new CoreInstaller());

                IocManager.Resolve<PlugInManager>().PlugInSources.AddRange(PlugInSources);
                IocManager.Resolve<StartupConfiguration>().Initialize();

                _moduleManager = IocManager.Resolve<ModuleManager>();
                _moduleManager.Initialize(StartupModule);
                _moduleManager.StartModules();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.ToString(), ex);
                throw;
            }
        }

        private void ResolveLogger()
        {
            if (IocManager.IsRegistered<ILoggerFactory>())
            {
                _logger = IocManager.Resolve<ILoggerFactory>().Create(typeof(Bootstrapper));
            }
        }

        private void RegisterBootstrapper()
        {
            if (!IocManager.IsRegistered<Bootstrapper>())
            {
                IocManager.IocContainer.Register(
                    Component.For<Bootstrapper>().Instance(this)
                    );
            }
        }

        /// <summary>
        /// Disposes the ABP system.
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            _moduleManager?.ShutdownModules();
        }
    }
}
