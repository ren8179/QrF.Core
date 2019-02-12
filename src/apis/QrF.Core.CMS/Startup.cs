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
using QrF.Core.ComFr.Mvc.Authorize;

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
            services.AddCors()
                .AddMvc()
                .AddJsonOptions(option => { option.SerializerSettings.DateFormatString = "yyyy-MM-dd"; })
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.TryAddScoped<IApplicationContext, CMSApplicationContext>();
            services.TryAddScoped<IApplicationContextAccessor, ApplicationContextAccessor>();
            services.TryAddTransient<INavigationService, NavigationService>();
            services.TryAddTransient<IArticleService, ArticleService>();
            services.TryAddTransient<IArticleTypeService, ArticleTypeService>();
            services.TryAddTransient<IMediaService, MediaService>();

            services.AddComFr(Configuration);
            
            services.AddScoped<IOnModelCreating, EntityFrameWorkModelCreating>();

            services.AddDbContextOptions<CMSDbContext>();
            services.AddDbContext<CMSDbContext>();
            services.AddScoped<ComFrDbContext>((provider) => provider.GetService<CMSDbContext>());
            services.AddAuthentication(DefaultAuthorizeAttribute.DefaultAuthenticationScheme)
                .AddCookie(DefaultAuthorizeAttribute.DefaultAuthenticationScheme, o =>
                {
                    o.LoginPath = new PathString("/CMSAPI/Account/Login");
                    o.AccessDeniedPath = new PathString("/Error/Forbidden");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            ServiceLocator.Setup(httpContextAccessor);
            Directory.SetCurrentDirectory(env.ContentRootPath);
            app.UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials())
                    .UseMvc();
        }
    }
}
