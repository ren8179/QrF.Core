using System.ComponentModel.DataAnnotations;

namespace QrF.Core.GatewayExtension.Entities
{
    /// <summary>
    /// 限流组策略表,记录分组的所有限流情况
    /// </summary>
    public class LimitGroupRule
    {
        /// <summary>
        /// 策略主键
        /// </summary>
        [Key]
        public int GroupRuleId { get; set; }
        /// <summary>
        /// 限流组主键
        /// </summary>
        public int? LimitGroupId { get; set; }
        /// <summary>
        /// 路由限流规则主键
        /// </summary>
        public int? ReRouteLimitId { get; set; }
    }
}
