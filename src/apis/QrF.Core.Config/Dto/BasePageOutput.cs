using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Config.Dto
{
    /// <summary>
    /// 分页返回基类
    /// </summary>
    public class BasePageOutput<T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { set; get; }
        /// <summary>
        /// 总条数
        /// </summary>
        public long Total { set; get; }
        public List<T> Rows { set; get; }
    }
}
