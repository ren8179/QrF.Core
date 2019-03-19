using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.GatewayExtension.Configuration
{
    /// <summary>
    /// Redis部署方式
    /// </summary>
    public enum RedisStoreMode
    {
        /// <summary>
        /// 普通方式
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 官方集群方式
        /// </summary>
        Cluster = 2,
        /// <summary>
        /// 哨兵方式
        /// </summary>
        Sentinel = 3,
        /// <summary>
        /// 分区方式
        /// </summary>
        Partition = 4
    }
}
