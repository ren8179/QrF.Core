using System.ComponentModel.DataAnnotations;

namespace QrF.Core.GatewayExtension.Entities
{
    /// <summary>
    /// 网关-路由,可以配置多个网关和多个路由
    /// </summary>
    public class ConfigReRoutes
    {
        /// <summary>
        /// 配置路由主键
        /// </summary>
        [Key]
        public int CtgRouteId { get; set; }
        /// <summary>
        /// 网关主键
        /// </summary>
        public int? KeyId { get; set; }
        /// <summary>
        /// 路由主键
        /// </summary>
        public int? ReRouteId { get; set; }
    }
}
