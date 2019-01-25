using System.ComponentModel.DataAnnotations;

namespace QrF.Core.GatewayExtension.Entities
{
    /// <summary>
    /// 限流组表,定义一系列限流规则，统一管理
    /// </summary>
    public class LimitGroup
    {
        /// <summary>
        /// 限流组主键
        /// </summary>
        [Key]
        public int LimitGroupId { get; set; }
        /// <summary>
        /// 限流组名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string LimitGroupName { get; set; }
        /// <summary>
        /// 限流组描述
        /// </summary>
        [StringLength(500)]
        public string LimitGroupDetail { get; set; }
        /// <summary>
        /// 当前状态, 1 有效 0 无效
        /// </summary>
        public int InfoStatus { get; set; }
    }
}
