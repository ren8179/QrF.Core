using System.ComponentModel.DataAnnotations;

namespace QrF.Core.GatewayExtension.Entities
{
    /// <summary>
    /// 限流规则表,记录限流的所有规则，可以对规则重复利用
    /// </summary>
    public class LimitRule
    {
        /// <summary>
        /// 规则主键
        /// </summary>
        [Key]
        public int RuleId { get; set; }
        /// <summary>
        /// 限流规则名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string LimitName { get; set; }
        /// <summary>
        /// 限流策略,支持 秒(s)  分钟(m) 小时( h) 天( d) , 比如10s
        /// </summary>
        [Required]
        [StringLength(50)]
        public string LimitPeriod { get; set; }
        /// <summary>
        /// 限制访问次数,大于0
        /// </summary>
        public int LimitNum { get; set; }
        /// <summary>
        /// 当前状态, 1 有效 0 无效
        /// </summary>
        public int InfoStatus { get; set; }
    }
}
