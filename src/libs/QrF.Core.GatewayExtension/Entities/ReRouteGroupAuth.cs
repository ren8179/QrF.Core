using System.ComponentModel.DataAnnotations;

namespace QrF.Core.GatewayExtension.Entities
{
    /// <summary>
    /// 授权组权限表,记录授权组能够访问的路由
    /// </summary>
    public class ReRouteGroupAuth
    {
        /// <summary>
        /// 授权主键
        /// </summary>
        [Key]
        public int AuthId { get; set; }
        /// <summary>
        /// 授权组主键
        /// </summary>
        public int? GroupId { get; set; }
        /// <summary>
        /// 路由主键
        /// </summary>
        public int? ReRouteId { get; set; }
    }
}
