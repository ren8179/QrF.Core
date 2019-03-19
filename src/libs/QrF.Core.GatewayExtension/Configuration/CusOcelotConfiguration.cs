using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.GatewayExtension.Configuration
{
    /// <summary>
    /// 自定义配置信息
    /// </summary>
    public class CusOcelotConfiguration
    {
        /// <summary>
        /// 数据库连接字符串，使用不同数据库时自行修改,默认实现了SQLSERVER
        /// </summary>
        public string DbConnectionStrings { get; set; }

        /// <summary>
        /// 是否启用定时器，默认不启动
        /// </summary>
        public bool EnableTimer { get; set; } = false;

        /// <summary>
        /// 定时器周期，单位（毫秒），默认30分总自动更新一次
        /// </summary>
        public int TimerDelay { get; set; } = 30 * 60 * 1000;

        /// <summary>
        /// Redis连接字符串
        /// </summary>
        public string RedisConnectionString { get; set; }
        /// <summary>
        /// 配置哨兵或分区时使用
        /// </summary>
        public string[] RedisSentinelOrPartitionConStr { get; set; }

        /// <summary>
        /// Redis部署方式，默认使用普通方式
        /// </summary>
        public RedisStoreMode RedisStoreMode { get; set; } = RedisStoreMode.Normal;

        /// <summary>
        /// Redis存储的key前缀,默认值Gateway,如果分布式缓存多个应用部署，需要修改此值。
        /// </summary>
        public string RedisOcelotKeyPrefix { get; set; } = "Ocelot_Gateway_";

        /// <summary>
        /// 是否启用集群环境，如果非集群环境直接本地缓存+数据库即可
        /// </summary>
        public bool ClusterEnvironment { get; set; } = false;

        /// <summary>
        /// 是否启用客户端授权,默认不开启
        /// </summary>
        public bool ClientAuthorization { get; set; } = false;
        /// <summary>
        /// 服务器缓存时间，默认30分钟
        /// </summary>
        public int CacheTime { get; set; } = 1800;
        /// <summary>
        /// 客户端标识，默认 client_id
        /// </summary>
        public string ClientKey { get; set; } = "client_id";

        /// <summary>
        /// 是否开启自定义限流，默认不开启
        /// </summary>
        public bool ClientRateLimit { get; set; } = false;
    }
}
