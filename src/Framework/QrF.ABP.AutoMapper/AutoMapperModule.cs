using AutoMapper;
using Castle.MicroKernel.Registration;
using QrF.ABP.Configuration.Startup;
using QrF.ABP.Modules;
using QrF.ABP.ObjectMapping;
using QrF.ABP.Reflection;
using System;
using System.Reflection;

namespace QrF.ABP.AutoMapper
{
    [DependsOn(typeof(KernelModule))]
    public class AutoMapperModule : BaseModule
    {
        private readonly ITypeFinder _typeFinder;

        private static volatile bool _createdMappingsBefore;
        private static readonly object SyncObj = new object();

        public AutoMapperModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public override void PreInitialize()
        {
            IocManager.Register<IAutoMapperConfiguration, AutoMapperConfiguration>();

            Configuration.ReplaceService<ObjectMapping.IObjectMapper, AutoMapperObjectMapper>();

        }

        public override void PostInitialize()
        {
            CreateMappings();
        }

        private void CreateMappings()
        {
            lock (SyncObj)
            {
                Action<IMapperConfigurationExpression> configurer = configuration =>
                {
                    FindAndAutoMapTypes(configuration);
                    foreach (var configurator in Configuration.Modules.AutoMapper().Configurators)
                    {
                        configurator(configuration);
                    }
                };

                if (Configuration.Modules.AutoMapper().UseStaticMapper)
                {
                    //We should prevent duplicate mapping in an application, since Mapper is static.
                    if (!_createdMappingsBefore)
                    {
                        Mapper.Initialize(configurer);
                        _createdMappingsBefore = true;
                    }

                    IocManager.IocContainer.Register(
                        Component.For<IConfigurationProvider>().Instance(Mapper.Configuration).LifestyleSingleton()
                    );
                    IocManager.IocContainer.Register(
                        Component.For<IMapper>().Instance(Mapper.Instance).LifestyleSingleton()
                    );
                }
                else
                {
                    var config = new MapperConfiguration(configurer);
                    IocManager.IocContainer.Register(
                        Component.For<IConfigurationProvider>().Instance(config).LifestyleSingleton()
                    );
                    IocManager.IocContainer.Register(
                        Component.For<IMapper>().Instance(config.CreateMapper()).LifestyleSingleton()
                    );
                }
            }
        }

        private void FindAndAutoMapTypes(IMapperConfigurationExpression configuration)
        {
            var types = _typeFinder.Find(type =>
            {
                var typeInfo = type.GetTypeInfo();
                return typeInfo.IsDefined(typeof(AutoMapAttribute)) ||
                       typeInfo.IsDefined(typeof(AutoMapFromAttribute)) ||
                       typeInfo.IsDefined(typeof(AutoMapToAttribute));
            }
            );

            Logger.DebugFormat("Found {0} classes define auto mapping attributes", types.Length);

            foreach (var type in types)
            {
                Logger.Debug(type.FullName);
                configuration.CreateAutoAttributeMaps(type);
            }
        }
    }
}
