using Castle.MicroKernel.Registration;
using QrF.ABP;
using QrF.ABP.Dependency;
using QrF.ABP.Domain.Repositories;
using QrF.ABP.Extensions;
using QrF.ABP.Modules;
using QrF.ABP.Reflection;
using QrF.ABP.Reflection.Extensions;
using QrF.Core.EntityFrameworkCore.Configuration;
using QrF.Core.EntityFrameworkCore.Repositories;
using System;
using System.Reflection;

namespace QrF.Core.EntityFrameworkCore
{
    [DependsOn(typeof(KernelModule))]
    public class EntityFrameworkCoreModule : BaseModule
    {
        private readonly ITypeFinder _typeFinder;

        public EntityFrameworkCoreModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public override void PreInitialize()
        {
            IocManager.Register<IEfCoreConfiguration, EfCoreConfiguration>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EntityFrameworkCoreModule).GetAssembly());
            
            RegisterGenericRepositoriesAndMatchDbContexes();
        }

        private void RegisterGenericRepositoriesAndMatchDbContexes()
        {
            var dbContextTypes =
                _typeFinder.Find(type =>
                {
                    var typeInfo = type.GetTypeInfo();
                    return typeInfo.IsPublic &&
                           !typeInfo.IsAbstract &&
                           typeInfo.IsClass &&
                           typeof(BaseDbContext).IsAssignableFrom(type);
                });

            if (dbContextTypes.IsNullOrEmpty())
            {
                Logger.Warn("No class found derived from BaseDbContext.");
                return;
            }

            using (IScopedIocResolver scope = IocManager.CreateScope())
            {
                foreach (var dbContextType in dbContextTypes)
                {
                    Logger.Debug("Registering DbContext: " + dbContextType.AssemblyQualifiedName);

                    scope.Resolve<IEfGenericRepositoryRegistrar>().RegisterForDbContext(dbContextType, IocManager, EfCoreAutoRepositoryTypes.Default);

                    IocManager.IocContainer.Register(
                        Component.For<ISecondaryOrmRegistrar>()
                            .Named(Guid.NewGuid().ToString("N"))
                            .Instance(new EfCoreBasedSecondaryOrmRegistrar(dbContextType, scope.Resolve<IDbContextEntityFinder>()))
                            .LifestyleTransient()
                    );
                }

                scope.Resolve<IDbContextTypeMatcher>().Populate(dbContextTypes);
            }
        }
    }
}
