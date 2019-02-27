using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QrF.Core.IdentityServer4.Dapper;
using QrF.Core.IdentityServer4.Dapper.SqlServer;
using QrF.Core.IdentityServer4.Validations;
using QrF.Core.Ids4.Infrastructure.Config;
using QrF.Core.Ids4.Infrastructure.Repositories;
using QrF.Core.Ids4.Services;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace QrF.Core.Ids4
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.Configure<ApiOptions>(Configuration);
            services.AddIdentityServer(option => {
                        option.PublicOrigin = Configuration["PublicOrigin"];
                    })
                .AddDeveloperSigningCredential()
                .AddDapperStore(o => {
                    o.DbConnectionStrings = Configuration["DbConnectionStrings"];
                    o.EnableForceExpire = true;
                    o.RedisConnectionStrings = new List<string>() { Configuration["RedisConnectionStrings"] };
                })
                .UseSqlServer()
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddProfileService<ProfileService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var container = new ContainerBuilder();
            container.RegisterModule<AutofacModuleRegister>();
            container.Populate(services);
            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
        /// <summary>
        /// Autofac扩展注册
        /// </summary>
        public class AutofacModuleRegister : Autofac.Module
        {
            /// <summary>
            /// 装载autofac方式注册
            /// </summary>
            /// <param name="builder"></param>
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterAssemblyTypes(typeof(UsersBusServices).GetTypeInfo().Assembly)
                    .Where(t => !t.IsAbstract && !t.IsInterface && t.Name.EndsWith("BusServices"))
                    .AsImplementedInterfaces().InstancePerLifetimeScope();
                builder.RegisterAssemblyTypes(typeof(UsersRepository).GetTypeInfo().Assembly)
                    .Where(t => !t.IsAbstract && !t.IsInterface && t.Name.EndsWith("Repository"))
                    .AsImplementedInterfaces().InstancePerLifetimeScope();
            }
        }
    }
}
