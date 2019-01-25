using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QrF.Core.GatewayExtension.Entities
{
    /// <summary>
    /// 网关全局配置表
    /// </summary>
    public class GlobalConfiguration
    {
        /// <summary>
        /// 网关主键
        /// </summary>
        [Key]
        public int KeyId { get; set; }
        /// <summary>
        /// 网关名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string GatewayName { get; set; }
        /// <summary>
        /// 全局请求默认key
        /// </summary>
        [StringLength(100)]
        public string RequestIdKey { get; set; }
        /// <summary>
        /// 请求路由根地址
        /// </summary>
        [StringLength(100)]
        public string BaseUrl { get; set; }
        /// <summary>
        /// 下游使用架构
        /// </summary>
        [StringLength(50)]
        public string DownstreamScheme { get; set; }
        /// <summary>
        /// 服务发现全局配置,值为配置json
        /// </summary>
        [StringLength(500)]
        public string ServiceDiscoveryProvider { get; set; }
        /// <summary>
        /// 全局负载均衡配置
        /// </summary>
        [StringLength(500)]
        public string LoadBalancerOptions { get; set; }
        /// <summary>
        /// Http请求配置
        /// </summary>
        [StringLength(500)]
        public string HttpHandlerOptions { get; set; }
        /// <summary>
        /// 请求安全配置,超时、重试、熔断
        /// </summary>
        [StringLength(200)]
        public string QoSOptions { get; set; }
        /// <summary>
        /// 是否默认配置, 1 默认 0 默认
        /// </summary>
        public int IsDefault { get; set; }
        /// <summary>
        /// 当前状态, 1 有效 0 无效
        /// </summary>
        public int InfoStatus { get; set; }
    }
}
