using Dapper;
using Newtonsoft.Json;
using Ocelot.Configuration.File;
using Ocelot.Configuration.Repository;
using Ocelot.Responses;
using QrF.Core.GatewayExtension.Configuration;
using QrF.Core.GatewayExtension.Entities;
using QrF.Core.Utils.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace QrF.Core.GatewayExtension.Dapper.SqlServer.Stores
{
    /// <summary>
    /// 使用SqlServer来实现配置文件仓储接口
    /// </summary>
    public class FileConfigurationRepository : IFileConfigurationRepository
    {
        private readonly CusOcelotConfiguration _option;
        public FileConfigurationRepository(CusOcelotConfiguration option)
        {
            _option = option;
        }

        /// <summary>
        /// 从数据库中获取配置信息
        /// </summary>
        /// <returns></returns>
        public async Task<Response<FileConfiguration>> Get()
        {
            #region 提取配置信息
            var file = new FileConfiguration();
            //提取默认启用的路由配置信息
            string glbsql = "select * from GlobalConfiguration where IsDefault=1 and InfoStatus=1";
            //提取全局配置信息
            using (var connection = new SqlConnection(_option.DbConnectionStrings))
            {
                var result = await connection.QueryFirstOrDefaultAsync<GlobalConfiguration>(glbsql);
                if (result != null)
                {
                    var glb = new FileGlobalConfiguration();
                    //赋值全局信息
                    glb.BaseUrl = result.BaseUrl;
                    glb.DownstreamScheme = result.DownstreamScheme;
                    glb.RequestIdKey = result.RequestIdKey;
                    if (!result.HttpHandlerOptions.IsNullOrEmpty())
                    {
                        glb.HttpHandlerOptions = result.HttpHandlerOptions.ToObject<FileHttpHandlerOptions>();
                    }
                    if (!result.LoadBalancerOptions.IsNullOrEmpty())
                    {
                        glb.LoadBalancerOptions = result.LoadBalancerOptions.ToObject<FileLoadBalancerOptions>();
                    }
                    if (!result.QoSOptions.IsNullOrEmpty())
                    {
                        glb.QoSOptions = result.QoSOptions.ToObject<FileQoSOptions>();
                    }
                    if (!result.ServiceDiscoveryProvider.IsNullOrEmpty())
                    {
                        glb.ServiceDiscoveryProvider = result.ServiceDiscoveryProvider.ToObject<FileServiceDiscoveryProvider>();
                    }
                    file.GlobalConfiguration = glb;

                    //提取所有路由信息
                    string routesql = "select T2.* from ConfigReRoutes T1 inner join ReRoute T2 on T1.ReRouteId=T2.ReRouteId where KeyId=@KeyId and InfoStatus=1";
                    var routeresult = (await connection.QueryAsync<ReRoute>(routesql, new { result.KeyId }))?.AsList();
                    if (routeresult != null && routeresult.Count > 0)
                    {
                        var reroutelist = new List<FileReRoute>();
                        foreach (var model in routeresult)
                        {
                            var m = new FileReRoute();
                            if (!model.AuthenticationOptions.IsNullOrEmpty())
                            {
                                m.AuthenticationOptions = model.AuthenticationOptions.ToObject<FileAuthenticationOptions>();
                            }
                            if (!model.CacheOptions.IsNullOrEmpty())
                            {
                                m.FileCacheOptions = model.CacheOptions.ToObject<FileCacheOptions>();
                            }
                            if (!model.DelegatingHandlers.IsNullOrEmpty())
                            {
                                m.DelegatingHandlers = model.DelegatingHandlers.ToObject<List<string>>();
                            }
                            if (!model.LoadBalancerOptions.IsNullOrEmpty())
                            {
                                m.LoadBalancerOptions = model.LoadBalancerOptions.ToObject<FileLoadBalancerOptions>();
                            }
                            if (!model.QoSOptions.IsNullOrEmpty())
                            {
                                m.QoSOptions = model.QoSOptions.ToObject<FileQoSOptions>();
                            }
                            if (!model.DownstreamHostAndPorts.IsNullOrEmpty())
                            {
                                m.DownstreamHostAndPorts = model.DownstreamHostAndPorts.ToObject<List<FileHostAndPort>>();
                            }
                            //开始赋值
                            m.DownstreamPathTemplate = model.DownstreamPathTemplate;
                            m.DownstreamScheme = model.DownstreamScheme;
                            m.Key = model.RequestIdKey;
                            m.Priority = model.Priority ?? 0;
                            m.RequestIdKey = model.RequestIdKey;
                            m.ServiceName = model.ServiceName;
                            m.UpstreamHost = model.UpstreamHost;
                            m.UpstreamHttpMethod = model.UpstreamHttpMethod?.ToObject<List<string>>();
                            m.UpstreamPathTemplate = model.UpstreamPathTemplate;
                            reroutelist.Add(m);
                        }
                        file.ReRoutes = reroutelist;
                    }
                }
                else
                {
                    throw new Exception("未监测到任何可用的配置信息");
                }
            }
            #endregion
            if (file.ReRoutes == null || file.ReRoutes.Count == 0)
            {
                return new OkResponse<FileConfiguration>(null);
            }
            return new OkResponse<FileConfiguration>(file);
        }
        
        public async Task<Response> Set(FileConfiguration fileConfiguration)
        {
            using (var con = new SqlConnection(_option.DbConnectionStrings))
            {
                var global = fileConfiguration?.GlobalConfiguration;
                if (global != null && !global.RequestIdKey.IsNullOrEmpty())
                {
                    var cmd = "UPDATE GlobalConfiguration SET BaseUrl=@BaseUrl,DownstreamScheme=@DownstreamScheme,ServiceDiscoveryProvider=@ServiceDiscoveryProvider,LoadBalancerOptions=@LoadBalancerOptions,HttpHandlerOptions=@HttpHandlerOptions,QoSOptions=@QoSOptions WHERE RequestIdKey=@RequestIdKey";
                    var result = await con.ExecuteAsync(cmd, new {
                        global.BaseUrl,global.DownstreamScheme, ServiceDiscoveryProvider=global.ServiceDiscoveryProvider.ToJson(),
                        LoadBalancerOptions = global.LoadBalancerOptions.ToJson(),
                        HttpHandlerOptions = global.HttpHandlerOptions.ToJson(),
                        QoSOptions = global.QoSOptions.ToJson(),
                        global.RequestIdKey
                    }, null, null, CommandType.Text);
                }
                var reRoutes = fileConfiguration.ReRoutes;
                if (reRoutes != null && reRoutes.Count > 0)
                {
                    foreach(var item in reRoutes)
                    {
                        var cmd = @"UPDATE ReRoute SET UpstreamPathTemplate=@UpstreamPathTemplate,UpstreamHttpMethod=@UpstreamHttpMethod,UpstreamHost=@UpstreamHost,DownstreamScheme=@DownstreamScheme,DownstreamPathTemplate=@DownstreamPathTemplate,
  DownstreamHostAndPorts=@DownstreamHostAndPorts,AuthenticationOptions=@AuthenticationOptions,CacheOptions=@CacheOptions,LoadBalancerOptions=@LoadBalancerOptions,QoSOptions=@QoSOptions,DelegatingHandlers=@DelegatingHandlers,ServiceName=@ServiceName WHERE RequestIdKey=@RequestIdKey";
                      var result = await con.ExecuteAsync(cmd, new {
                            item.UpstreamPathTemplate,item.UpstreamHost,item.DownstreamScheme,item.DownstreamPathTemplate,
                            UpstreamHttpMethod =item.UpstreamHttpMethod.ToJson(),
                            DownstreamHostAndPorts = item.DownstreamHostAndPorts.ToJson(),
                            AuthenticationOptions = item.AuthenticationOptions.ToJson(),
                            CacheOptions = item.FileCacheOptions.ToJson(),
                            LoadBalancerOptions = item.LoadBalancerOptions.ToJson(),
                            QoSOptions = item.QoSOptions.ToJson(),
                            DelegatingHandlers = item.DelegatingHandlers.ToJson(),
                            item.ServiceName,
                            item.RequestIdKey
                        }, null, null, CommandType.Text);
                    }
                }
            }


            return await Task.FromResult<Response>(new OkResponse());
        }
    }
}
