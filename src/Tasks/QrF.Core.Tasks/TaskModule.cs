using Autofac;
using AutoMapper;
using QrF.Core.Domain.Contracts;
using QrF.Core.Infrastructure.Cqrs.Commands;
using QrF.Core.Infrastructure.Cqrs.Queries;
using System.Reflection;

namespace QrF.Core.Materials
{
    public class TaskModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new MapperConfiguration(cfg =>
            {

            }).CreateMapper());
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .Where(x => x.IsAssignableTo<IStorage>())
                   .AsImplementedInterfaces()
                   .SingleInstance();
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().InstancePerLifetimeScope();
            builder.RegisterType<QueryExecutor>().As<IQueryExecutor>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsClosedTypesOf(typeof(IQueryHandler<,>)).InstancePerLifetimeScope();
        }
    }
}
