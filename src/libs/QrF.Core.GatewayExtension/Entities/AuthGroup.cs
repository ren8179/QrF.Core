using System.ComponentModel.DataAnnotations;

namespace QrF.Core.GatewayExtension.Entities
{
    /// <summary>
    /// 授权组表,记录授权组里可访问的权限
    /// </summary>
    public class AuthGroup
    {
        /// <summary>
        /// 授权组主键
        /// </summary>
        [Key]
        public int GroupId { get; set; }
        /// <summary>
        /// 授权组名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string GroupName { get; set; }
        /// <summary>
        /// 授权组描述
        /// </summary>
        [StringLength(500)]
        public string GroupDetail { get; set; }
        /// <summary>
        /// 当前状态, 1 有效 0 无效
        /// </summary>
        public int InfoStatus { get; set; }
    }
}
