using Castle.MicroKernel.Registration;
using Microsoft.AspNetCore.Mvc;
using QrF.ABP.Dependency;
using System.Reflection;

namespace QrF.ABP.AspNetCore
{
    public class AspNetCoreConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            //ViewComponents
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .BasedOn<ViewComponent>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .LifestyleTransient()
            );
        }
    }
}
