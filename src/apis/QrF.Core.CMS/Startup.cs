using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using QrF.Core.CMS.Entities;
using QrF.Core.CMS.Service;
using QrF.Core.ComFr;
using QrF.Core.ComFr.DbConnectionPool;
using QrF.Core.ComFr.Repositories;
using QrF.Core.ComFr.DependencyInjection;
using QrF.Core.ComFr.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace QrF.Core.CMS
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
        public void ConfigureServices(IServiceCollection services)
        {
           services.AddMvc()
            .AddJsonOptions(option => { option.SerializerSettings.DateFormatString = "yyyy-MM-dd"; })
            .SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.TryAddScoped<IApplicationContext, CMSApplicationContext>();
            services.TryAddTransient<INavigationService, NavigationService>();
            services.AddComFr(Configuration);
            
            services.AddScoped<IOnModelCreating, EntityFrameWorkModelCreating>();

            services.AddDbContextOptions<CMSDbContext>();
            services.AddDbContext<CMSDbContext>();
            services.AddScoped<ComFrDbContext>((provider) => provider.GetService<CMSDbContext>());
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            ServiceLocator.Setup(httpContextAccessor);
            Directory.SetCurrentDirectory(env.ContentRootPath);
            app.UseMvc();
        }
    }
}
