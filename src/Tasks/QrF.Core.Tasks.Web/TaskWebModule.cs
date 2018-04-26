using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using QrF.ABP.AspNetCore.Configuration;
using QrF.ABP.Modules;
using QrF.ABP.Reflection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QrF.Core.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore;

namespace QrF.Core.Tasks.Web
{
    [DependsOn(typeof(TaskModule))]
    public class TaskWebModule : BaseModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = IocManager.Resolve<IConfigurationRoot>().GetConnectionString("Default");

            Configuration.Modules.AbpEfCore().AddDbContext<TaskDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    options.DbContextOptions.UseSqlServer(options.ExistingConnection);
                }
                else
                {
                    options.DbContextOptions.UseSqlServer(options.ConnectionString);
                }
            });

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(TaskWebModule).GetAssembly()
                );


            Configuration.IocManager.Resolve<IAspNetCoreConfiguration>().RouteConfiguration.Add(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TaskWebModule).GetAssembly());
        }
    }
}
