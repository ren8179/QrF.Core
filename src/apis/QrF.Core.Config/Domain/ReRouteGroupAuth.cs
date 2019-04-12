using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Config.Domain
{
    /// <summary>
    /// 授权组权限表,记录授权组能够访问的路由
    /// </summary>
    [SugarTable("ReRouteGroupAuth")]
    public class ReRouteGroupAuth
    {
        /// <summary>
        /// 授权主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
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
