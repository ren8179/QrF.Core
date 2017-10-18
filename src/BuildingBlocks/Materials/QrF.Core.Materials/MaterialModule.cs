using Autofac;
using Autofac.Features.Variance;
using MediatR;
using QrF.Core.Domain.Contracts;
using QrF.Core.Infrastructure.Cqrs.Commands;
using QrF.Core.Infrastructure.Cqrs.Queries;
using QrF.Core.Materials.Infrastructure.Configuration;
using System.Reflection;

namespace QrF.Core.Materials
{
    public class MaterialModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.Initialize());
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .Where(x => x.IsAssignableTo<IStorage>())
                   .AsImplementedInterfaces()
                   .SingleInstance();
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().InstancePerLifetimeScope();
            builder.RegisterType<QueryExecutor>().As<IQueryExecutor>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsClosedTypesOf(typeof(IQueryHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterType<Mediator>().As<IMediator>().SingleInstance();
            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => { object o; return c.TryResolve(t, out o) ? o : null; };
            }).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsClosedTypesOf(typeof(IAsyncRequestHandler<,>))
                .InstancePerLifetimeScope();
        }
    }
}
