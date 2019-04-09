using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using QrF.Core.Config.Extension;
using QrF.Core.Config.Infrastructure.DbContext;
using QrF.Core.Config.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace QrF.Core.Config
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSqlSugarDbContext();
            services.AddAutoMapper();
            services.AddCors();
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(WebApiResultMiddleware));
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule<AutofacModuleRegister>();
            return new AutofacServiceProvider(builder.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Program.BasePath + "/wwwroot/"),
                RequestPath = new PathString("/ConfigAPI")
            });
            app.UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
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
                // 业务应用注册
                builder.RegisterAssemblyTypes(typeof(IClientsBusiness).GetTypeInfo().Assembly)
                    .Where(t => !t.IsAbstract && !t.IsInterface && t.Name.EndsWith("Business"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            }
        }
    }
}
