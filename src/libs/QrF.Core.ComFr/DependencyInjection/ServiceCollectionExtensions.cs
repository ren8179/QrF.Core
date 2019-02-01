using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using QrF.Core.ComFr.Cache;
using QrF.Core.ComFr.DbConnectionPool;
using QrF.Core.ComFr.Modules.User.Service;
using QrF.Core.ComFr.Mvc;
using QrF.Core.ComFr.Mvc.Authorize;
using QrF.Core.ComFr.Mvc.StateProviders;
using QrF.Core.ComFr.Options;
using QrF.Core.ComFr.Storage;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QrF.Core.ComFr.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddComFr(this IServiceCollection services, IConfiguration configuration)
        {

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddTransient<IUserService, UserService>();

            services.AddTransient<IStorage, WebStorage>();

            services.ConfigureCache<ConcurrentDictionary<string, object>>();

            services.AddSingleton<IDatabaseConfiguring, EntityFrameWorkConfigure>();
            services.AddSingleton<IDbConnectionPool, SimpleDbConnectionPool>();
            
            services.AddScoped<IApplicationContextStateProvider, CurrentUserStateProvider>();
            services.AddScoped<IApplicationContextStateProvider, HostingEnvironmentStateProvider>();

            //池的配置：
            //MaximumRetained规定池的容量（常态最大保有数量）。
            //MaximumRetained为0时，相当于不使用DbConnection池，
            //但因为在Request期间Connection是保持打开的，所以对许多场合还是有性能改善的。
            services.AddSingleton(new ComFr.DbConnectionPool.Options() { MaximumRetained = 128 });
            //提供在Request期间租、还DbConnection的支持
            services.AddScoped<IConnectionHolder, TransientConnectionHolder>();
            DatabaseOption databaseOption = configuration.GetSection("Database").Get<DatabaseOption>();
            services.AddSingleton(databaseOption);
            services.AddDataProtection();
        }
        
        public static IServiceCollection ConfigureCache<T>(this IServiceCollection services)
        {
            return services.AddScoped(serviceProvider => serviceProvider.GetService<ICacheProvider>().Build<T>());
        }

        public static void AddDbContextOptions<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
        {
            services.AddScoped<DbContextOptions<TDbContext>>(sp =>
            {
                IConnectionHolder holder = sp.GetService<IConnectionHolder>();
                IDatabaseConfiguring configure = sp.GetService<IDatabaseConfiguring>();
                DbContextOptionsBuilder<TDbContext> optBuilder = new DbContextOptionsBuilder<TDbContext>();
                configure.OnConfiguring(optBuilder, holder.DbConnection);
                return optBuilder.Options;
            });
        }
    }
}
