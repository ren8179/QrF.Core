using Autofac;
using Autofac.Features.Variance;
using MediatR;
using System.Reflection;

namespace QrF.Core.Infrastructure.Modules
{
    public class MediatRModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterType<Mediator>().As<IMediator>().SingleInstance();
            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => { object o; return c.TryResolve(t, out o) ? o : null; };
            }).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(MediatRModule).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IAsyncRequestHandler<,>))
                .InstancePerLifetimeScope();
        }
    }
}
