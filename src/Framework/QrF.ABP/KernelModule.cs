using Castle.MicroKernel.Registration;
using QrF.ABP.Configuration;
using QrF.ABP.Configuration.Startup;
using QrF.ABP.Dependency;
using QrF.ABP.Events.Bus;
using QrF.ABP.Modules;
using QrF.ABP.Reflection.Extensions;
using QrF.ABP.Runtime;
using QrF.ABP.Runtime.Remoting;
using System;

namespace QrF.ABP
{
    /// <summary>
    /// Kernel (core) module of the system.
    /// No need to depend on this, it's automatically the first module always.
    /// </summary>
    public sealed class KernelModule : BaseModule
    {
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());

            IocManager.Register<IScopedIocResolver, ScopedIocResolver>(DependencyLifeStyle.Transient);
            IocManager.Register(typeof(IAmbientScopeProvider<>), typeof(DataContextAmbientScopeProvider<>), DependencyLifeStyle.Transient);
            
            AddSettingProviders();
            ConfigureCaches();
        }

        public override void Initialize()
        {
            foreach (var replaceAction in ((StartupConfiguration)Configuration).ServiceReplaceActions.Values)
            {
                replaceAction();
            }

            IocManager.IocContainer.Install(new EventBusInstaller(IocManager));

            IocManager.RegisterAssemblyByConvention(typeof(KernelModule).GetAssembly(),
                new ConventionalRegistrationConfig
                {
                    InstallInstallers = false
                });
        }

        public override void PostInitialize()
        {
            RegisterMissingComponents();

            IocManager.Resolve<SettingDefinitionManager>().Initialize();

        }

        public override void Shutdown()
        {

        }

        private void AddSettingProviders()
        {

        }

        private void ConfigureCaches()
        {
            Configuration.Caching.Configure(CacheManagerSettingExtensions.ApplicationSettings, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(8);
            });
        }

        private void RegisterMissingComponents()
        {
            if (!IocManager.IsRegistered<IGuidGenerator>())
            {
                IocManager.IocContainer.Register(
                    Component
                        .For<IGuidGenerator, SequentialGuidGenerator>()
                        .Instance(SequentialGuidGenerator.Instance)
                );
            }
           
        }
    }
}
