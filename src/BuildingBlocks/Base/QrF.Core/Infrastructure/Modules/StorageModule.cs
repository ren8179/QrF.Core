using Autofac;
using QrF.Core.Domain.Contracts;
using System.Reflection;

namespace QrF.Core.Infrastructure.Modules
{
    public class StorageModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(StorageModule).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(assembly)
                   .Where(x => x.IsAssignableTo<IStorage>())
                   .AsImplementedInterfaces()
                   .SingleInstance();
        }
    }
}
