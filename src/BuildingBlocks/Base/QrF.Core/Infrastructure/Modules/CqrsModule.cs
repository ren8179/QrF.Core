using Autofac;
using QrF.Core.Infrastructure.Cqrs.Commands;
using QrF.Core.Infrastructure.Cqrs.Queries;
using System.Reflection;

namespace QrF.Core.Infrastructure.Modules
{
    internal class CqrsModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(CqrsModule).GetTypeInfo().Assembly;
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().InstancePerLifetimeScope();
            builder.RegisterType<QueryExecutor>().As<IQueryExecutor>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IQueryHandler<,>)).InstancePerLifetimeScope();
        }
    }
}
