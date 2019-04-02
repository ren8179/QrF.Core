using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Config.Infrastructure.DbContext
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加多数据库 DbContext
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlSugarDbContext(this IServiceCollection services, ServiceLifetime contextLifetime = ServiceLifetime.Scoped)
        {
            var service = services.First(x => x.ServiceType == typeof(IConfiguration));
            var configuration = (IConfiguration)service.ImplementationInstance;
            var connectOptions = configuration.GetSection("DbConfig").Get<List<DbConnectOption>>();
            if (connectOptions != null)
            {
                connectOptions.ForEach(option =>
                {
                    QrfSqlSugarClient func(IServiceProvider s)
                    {
                        return new QrfSqlSugarClient(new ConnectionConfig
                        {
                            ConnectionString = option.ConnectionString,
                            DbType = option.DbType,
                            IsAutoCloseConnection = option.IsAutoCloseConnection,
                            InitKeyType = InitKeyType.Attribute
                        }, option.Name, option.Default);
                    }
                    if (contextLifetime == ServiceLifetime.Scoped)
                        services.AddScoped(func);
                    if (contextLifetime == ServiceLifetime.Singleton)
                        services.AddSingleton(func);
                    if (contextLifetime == ServiceLifetime.Transient)
                        services.AddTransient(func);
                });
            }
            return services;
        }
    }
}
