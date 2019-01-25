using System.ComponentModel.DataAnnotations;

namespace QrF.Core.GatewayExtension.Entities
{
    /// <summary>
    /// 客户端授权限流组
    /// </summary>
    public class ClientLimitGroup
    {
        /// <summary>
        /// 客户端限流组主键
        /// </summary>
        [Key]
        public int ClientLimitGroupId { get; set; }
        /// <summary>
        /// 客户端主键
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 限流组主键
        /// </summary>
        public int? LimitGroupId { get; set; }
    }
}
