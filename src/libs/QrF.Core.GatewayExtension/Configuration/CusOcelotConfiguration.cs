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
        public List<string> RedisConnectionStrings { get; set; }

        /// <summary>
        /// Redis存储的key前缀,默认值Gateway,如果分布式缓存多个应用部署，需要修改此值。
        /// </summary>
        public string RedisKeyPrefix { get; set; } = "Gateway";

        /// <summary>
        /// 是否启用客户端授权,默认不开启
        /// </summary>
        public bool ClientAuthorization { get; set; } = false;

        /// <summary>
        /// 客户端授权缓存时间，默认30分钟，单位：秒
        /// </summary>
        public int ClientAuthorizationCacheTime { get; set; } = 1800;
        /// <summary>
        /// 客户端标识，默认 client_id
        /// </summary>
        public string ClientKey { get; set; } = "client_id";

        /// <summary>
        /// 是否开启自定义限流，默认不开启
        /// </summary>
        public bool ClientRateLimit { get; set; } = false;

        /// <summary>
        /// 客户端限流缓存时间，默认30分钟，单位：秒
        /// </summary>
        public int ClientRateLimitCacheTime { get; set; } = 1800;
    }
}
