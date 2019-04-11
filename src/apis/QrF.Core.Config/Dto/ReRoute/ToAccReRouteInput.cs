using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Config.Dto
{
    public class ToAccReRouteInput
    {
        /// <summary>
        /// 网关主键
        /// </summary>
        public int? KeyId { get; set; }
        /// <summary>
        /// 路由主键
        /// </summary>
        public int? ReRouteId { get; set; }
        /// <summary>
        /// 类型 1 授权；0 取消授权
        /// </summary>
        public int Type { get; set; }
    }
}
