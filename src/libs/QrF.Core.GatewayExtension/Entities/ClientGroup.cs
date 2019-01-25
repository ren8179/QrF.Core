using System.ComponentModel.DataAnnotations;

namespace QrF.Core.GatewayExtension.Entities
{
    /// <summary>
    /// 客户端配置授权组表
    /// </summary>
    public class ClientGroup
    {
        /// <summary>
        /// 客户端授权主键
        /// </summary>
        [Key]
        public int ClientGroupId { get; set; }
        /// <summary>
        /// 客户端主键
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 授权组主键
        /// </summary>
        public int? GroupId { get; set; }
    }
}
