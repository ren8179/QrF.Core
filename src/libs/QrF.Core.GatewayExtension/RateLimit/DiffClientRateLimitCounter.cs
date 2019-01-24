using Newtonsoft.Json;
using System;

namespace QrF.Core.GatewayExtension.RateLimit
{
    /// <summary>
    /// 客户端限流计数器
    /// </summary>
    public struct DiffClientRateLimitCounter
    {
        [JsonConstructor]
        public DiffClientRateLimitCounter(DateTime timestamp, long totalRequests)
        {
            Timestamp = timestamp;
            TotalRequests = totalRequests;
        }

        /// <summary>
        /// 最后请求时间
        /// </summary>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// 请求总数
        /// </summary>
        public long TotalRequests { get; private set; }
    }
}
