using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QrF.Core.IdentityServer.Data;
using System;
using System.IO;

namespace QrF.Core.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "IdentityServer";
            BuildWebHost(args)
                .MigrateDbContext<PersistedGrantDbContext>((_, __) => { })
                .MigrateDbContext<ConfigurationDbContext>((context, services) =>
                {
                    var configuration = services.GetService<IConfiguration>();

                    new ConfigurationDbContextSeed()
                        .SeedAsync(context, configuration)
                        .Wait();
                }).MigrateDbContext<ApplicationDbContext>((context, services) =>
                {
                    var env = services.GetService<IHostingEnvironment>();
                    var logger = services.GetService<ILogger<ApplicationDbContextSeed>>();

                    new ApplicationDbContextSeed()
                        .SeedAsync(context, env, logger)
                        .Wait();
                }).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .UseKestrel()
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseIISIntegration()
               .UseStartup<Startup>()
               .ConfigureAppConfiguration((builderContext, config) =>
               {
                   config.AddEnvironmentVariables();
               })
               .ConfigureLogging((hostingContext, builder) =>
               {
                   builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                   builder.AddConsole();
                   builder.AddDebug();
               })
               .Build();
    }
}
