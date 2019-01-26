using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.IdentityServer4.Dapper.Options
{
    /// <summary>
    /// 配置存储信息
    /// </summary>
    public class DapperStoreOptions
    {
        /// <summary>
        /// 是否启用定时清理Token
        /// </summary>
        public bool EnableTokenCleanup { get; set; } = false;

        /// <summary>
        /// 清理token周期（单位秒），默认1小时
        /// </summary>
        public int TokenCleanupInterval { get; set; } = 3600;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string DbConnectionStrings { get; set; }

        /// <summary>
        /// 是否启用强制过期策略,默认不开启
        /// </summary>
        public bool EnableForceExpire { get; set; } = false;

        /// <summary>
        /// Redis缓存连接
        /// </summary>
        public List<string> RedisConnectionStrings { get; set; }
    }
}
