using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.Administration;
using Ocelot.DependencyInjection;
using QrF.Core.Gateway.Extension;
using QrF.Core.GatewayExtension.Dapper.SqlServer;
using QrF.Core.GatewayExtension.DependencyInjection;
using QrF.Core.GatewayExtension.Middleware;
using System;
using System.Collections.Generic;

namespace QrF.Core.Gateway
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
            Action<IdentityServerAuthenticationOptions> config = o =>
            {
                o.Authority = Configuration["Auth:ServerUrl"];
                o.ApiName = "gateway";
                o.RequireHttpsMetadata = false;
            };
            services.AddAuthentication()
                .AddIdentityServerAuthentication(Configuration["Auth:ProviderKey"], config);

            services.AddCustomSwagger(Configuration);
            services.AddOcelot(Configuration).AddExtOcelot(option =>
            {
                option.DbConnectionStrings = Configuration["OcelotConfig:DbConnectionStrings"];
                option.RedisConnectionStrings = new List<string>() {
                    Configuration["OcelotConfig:RedisConnectionStrings"]
                };
                option.EnableTimer = Convert.ToBoolean(Configuration["OcelotConfig:EnableTimer"]);
                option.TimerDelay = Convert.ToInt32(Configuration["OcelotConfig:TimerDelay"]);
                option.ClientAuthorization = true;
                option.ClientRateLimit = true;
            })
            .UseSqlServer()
            .AddAdministration("/ocelot", config);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseExtOcelot().Wait();
        }
    }
}
