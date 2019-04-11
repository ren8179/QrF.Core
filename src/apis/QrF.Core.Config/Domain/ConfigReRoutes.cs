using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Config.Domain
{
    /// <summary>
    /// 网关-路由,可以配置多个网关和多个路由
    /// </summary>
    [SugarTable("ConfigReRoutes")]
    public class ConfigReRoutes
    {
        /// <summary>
        /// 配置路由主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
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
