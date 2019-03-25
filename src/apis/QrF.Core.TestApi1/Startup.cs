using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QrF.Core.TestApi1.Configuration;
using QrF.Core.TestApi1.Filters;
using QrF.Core.Utils.Extension;
using System;

namespace QrF.Core.TestApi1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddAuthentication(BearerAuthorizeAttribute.DefaultAuthenticationScheme)
            .AddCookie(BearerAuthorizeAttribute.DefaultAuthenticationScheme, o =>
            {
                o.Cookie.Name = BearerAuthorizeAttribute.DefaultAuthenticationScheme;
                o.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            });
            services.Configure<AppSettings>(Configuration)
                .AddMvc(options=> {
                var url = Configuration["Auth:ServerUrl"];
                var apiName = Configuration["Auth:ApiName"];
                if (!url.IsNullOrEmpty() && !apiName.IsNullOrEmpty())
                    options.Filters.Add(typeof(BearerAuthorizeAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
