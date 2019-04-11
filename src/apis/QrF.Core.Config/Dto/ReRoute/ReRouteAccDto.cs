using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Config.Dto
{
    public class ReRouteAccDto
    {
        /// <summary>
        /// 路由主键
        /// </summary>
        public int ReRouteId { get; set; }
        /// <summary>
        /// 分类主键
        /// </summary>
        public int? ItemId { get; set; }
        /// <summary>
        /// 上游路径模板，支持正则
        /// </summary>
        public string UpstreamPathTemplate { get; set; }
        /// <summary>
        /// 下游路径模板,与上游正则对应
        /// </summary>
        public string DownstreamPathTemplate { get; set; }
        /// <summary>
        /// 下游请求地址和端口,静态负载配置
        /// </summary>
        public string DownstreamHostAndPorts { get; set; }
        /// <summary>
        /// 请求默认key
        /// </summary>
        public string RequestIdKey { get; set; }
        /// <summary>
        /// 路由优先级,多个路由匹配上，优先级高的先执行
        /// </summary>
        public int? Priority { get; set; }
        /// <summary>
        /// 当前状态, 1 有效 0 无效'
        /// </summary>
        public int InfoStatus { get; set; }
        public bool IsAuth { get; set; }
    }
}
