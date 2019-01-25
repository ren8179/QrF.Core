using System.ComponentModel.DataAnnotations;

namespace QrF.Core.GatewayExtension.Entities
{
    /// <summary>
    /// 路由限流规则表
    /// </summary>
    public class ReRouteLimitRule
    {
        /// <summary>
        /// 路由限流规则主键
        /// </summary>
        [Key]
        public int ReRouteLimitId { get; set; }
        /// <summary>
        /// 规则主键
        /// </summary>
        public int? RuleId { get; set; }
        /// <summary>
        /// 路由主键
        /// </summary>
        public int? ReRouteId { get; set; }
    }
}
