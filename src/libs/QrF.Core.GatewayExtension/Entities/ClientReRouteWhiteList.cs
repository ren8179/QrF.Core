using System.ComponentModel.DataAnnotations;

namespace QrF.Core.GatewayExtension.Entities
{
    /// <summary>
    /// 客户端路由白名单
    /// </summary>
    public class ClientReRouteWhiteList
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int WhiteListId { get; set; }
        /// <summary>
        /// 路由主键
        /// </summary>
        public int? ReRouteId { get; set; }
        /// <summary>
        /// 客户端主键
        /// </summary>
        public int? Id { get; set; }
    }
}
