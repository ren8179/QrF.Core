using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Config.Dto
{
    public class ToAccGroupInput
    {
        /// <summary>
        /// 
        /// </summary>
        public int? KeyId { get; set; }
        /// <summary>
        /// 授权组主键
        /// </summary>
        public int? GroupId { get; set; }
        /// <summary>
        /// 类型 1 授权；0 取消授权
        /// </summary>
        public int Type { get; set; }
    }
}
