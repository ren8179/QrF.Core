using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Dto
{
    /// <summary>
    /// 分页返回基类
    /// </summary>
    public class BasePageOutput
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { set; get; }
        /// <summary>
        /// 总条数
        /// </summary>
        public long Total { set; get; }
    }
}
