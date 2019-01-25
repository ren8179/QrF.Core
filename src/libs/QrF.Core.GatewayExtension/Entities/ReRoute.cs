using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QrF.Core.GatewayExtension.Entities
{
    /// <summary>
    /// 路由配置表
    /// </summary>
    public class ReRoute
    {
        /// <summary>
        /// 路由主键
        /// </summary>
        [Key]
        public int ReRouteId { get; set; }
        /// <summary>
        /// 分类主键
        /// </summary>
        public int? ItemId { get; set; }
        /// <summary>
        /// 上游路径模板，支持正则
        /// </summary>
        [Required]
        [StringLength(150)]
        public string UpstreamPathTemplate { get; set; }
        /// <summary>
        /// 上游请求方法数组格式
        /// </summary>
        [Required]
        [StringLength(50)]
        public string UpstreamHttpMethod { get; set; }
        /// <summary>
        /// 上游域名地址
        /// </summary>
        [StringLength(100)]
        public string UpstreamHost { get; set; }
        /// <summary>
        /// 下游使用架构
        /// </summary>
        [Required]
        [StringLength(50)]
        public string DownstreamScheme { get; set; }
        /// <summary>
        /// 下游路径模板,与上游正则对应
        /// </summary>
        [Required]
        [StringLength(200)]
        public string DownstreamPathTemplate { get; set; }
        /// <summary>
        /// 下游请求地址和端口,静态负载配置
        /// </summary>
        [StringLength(500)]
        public string DownstreamHostAndPorts { get; set; }
        /// <summary>
        /// 授权配置,是否需要认证访问
        /// </summary>
        [StringLength(300)]
        public string AuthenticationOptions { get; set; }
        /// <summary>
        /// 全局请求默认key
        /// </summary>
        [StringLength(100)]
        public string RequestIdKey { get; set; }
        /// <summary>
        /// 缓存配置,常用查询和再次配置缓存
        /// </summary>
        [StringLength(200)]
        public string CacheOptions { get; set; }
        /// <summary>
        /// 服务发现名称,启用服务发现时生效
        /// </summary>
        [StringLength(100)]
        public string ServiceName { get; set; }
        /// <summary>
        /// 全局负载均衡配置
        /// </summary>
        [StringLength(500)]
        public string LoadBalancerOptions { get; set; }
        /// <summary>
        /// 请求安全配置,超时、重试、熔断
        /// </summary>
        [StringLength(200)]
        public string QoSOptions { get; set; }
        /// <summary>
        /// 委托处理方法,特定路由定义委托单独处理
        /// </summary>
        [StringLength(200)]
        public string DelegatingHandlers { get; set; }
        /// <summary>
        /// 路由优先级,多个路由匹配上，优先级高的先执行
        /// </summary>
        public int? Priority { get; set; }
        /// <summary>
        /// 当前状态, 1 有效 0 无效'
        /// </summary>
        public int InfoStatus { get; set; }
    }
}
