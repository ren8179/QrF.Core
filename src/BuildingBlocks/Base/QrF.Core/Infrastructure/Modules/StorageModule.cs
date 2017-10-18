using Autofac;
using QrF.Core.Domain.Contracts;
using System.Reflection;

namespace QrF.Core.Infrastructure.Modules
{
    public class StorageModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .Where(x => x.IsAssignableTo<IStorage>())
                   .AsImplementedInterfaces()
                   .SingleInstance();
        }
    }
}
