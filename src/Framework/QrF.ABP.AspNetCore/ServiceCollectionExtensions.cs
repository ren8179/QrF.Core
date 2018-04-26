using Castle.Windsor.MsDependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using QrF.ABP.AspNetCore.Mvc.Antiforgery;
using QrF.ABP.Dependency;
using QrF.ABP.Json;
using QrF.ABP.Modules;
using QrF.ABP.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Razor;

namespace QrF.ABP.AspNetCore
{
    public static class AbpServiceCollectionExtensions
    {
        /// <summary>
        /// Integrates ABP to AspNet Core.
        /// </summary>
        /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="AbpModule"/>.</typeparam>
        /// <param name="services">Services.</param>
        /// <param name="optionsAction">An action to get/modify options</param>
        public static IServiceProvider AddAbp<TStartupModule>(this IServiceCollection services, Action<BootstrapperOptions> optionsAction = null)
            where TStartupModule : BaseModule
        {
            var bootstrapper = AddBootstrapper<TStartupModule>(services, optionsAction);

            ConfigureAspNetCore(services, bootstrapper.IocManager);

            return WindsorRegistrationHelper.CreateServiceProvider(bootstrapper.IocManager.IocContainer, services);
        }

        private static void ConfigureAspNetCore(IServiceCollection services, IIocResolver iocResolver)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //Use DI to create controllers
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            //Use DI to create view components
            services.Replace(ServiceDescriptor.Singleton<IViewComponentActivator, ServiceBasedViewComponentActivator>());

            //Change anti forgery filters (to work proper with non-browser clients)
            services.Replace(ServiceDescriptor.Transient<AutoValidateAntiforgeryTokenAuthorizationFilter, BaseAutoValidateAntiforgeryTokenAuthorizationFilter>());
            services.Replace(ServiceDescriptor.Transient<ValidateAntiforgeryTokenAuthorizationFilter, BaseValidateAntiforgeryTokenAuthorizationFilter>());
            
            //Configure JSON serializer
            services.Configure<MvcJsonOptions>(jsonOptions =>
            {
                jsonOptions.SerializerSettings.ContractResolver = new ContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });

            //Configure MVC
            services.Configure<MvcOptions>(mvcOptions =>
            {
                mvcOptions.AddAbp(services);
            });

            //Configure Razor
            services.Insert(0,
                ServiceDescriptor.Singleton<IConfigureOptions<RazorViewEngineOptions>>(
                    new ConfigureOptions<RazorViewEngineOptions>(
                        (options) =>
                        {
                            
                        }
                    )
                )
            );
        }

        private static Bootstrapper AddBootstrapper<TStartupModule>(IServiceCollection services, Action<BootstrapperOptions> optionsAction)
            where TStartupModule : BaseModule
        {
            var abpBootstrapper = Bootstrapper.Create<TStartupModule>(optionsAction);
            services.AddSingleton(abpBootstrapper);
            return abpBootstrapper;
        }
    }
}
