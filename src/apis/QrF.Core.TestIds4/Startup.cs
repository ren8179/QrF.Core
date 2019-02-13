using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QrF.Core.IdentityServer4.Dapper;
using QrF.Core.IdentityServer4.Dapper.SqlServer;
using System.Collections.Generic;

namespace QrF.Core.TestIds4
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
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddDapperStore(o => o.DbConnectionStrings = "Server=.;Database=GatewayDb;User ID=sa;Password=pass;")
                .UseSqlServer();
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
            app.UseMvcWithDefaultRoute();
        }
    }
    public class Config
    {
        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("gateway", "gateway API"),
                new ApiResource("gateway_admin", "gateway admin API")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client1",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("gateway_secret1".Sha256())
                    },
                    AllowedScopes = { "gateway" }
                },
                new Client
                {
                    ClientId = "client2",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("gateway_secret2".Sha256())
                    },
                    AllowedScopes = { "gateway", "gateway_admin" }
                }
            };
        }
    }
}
