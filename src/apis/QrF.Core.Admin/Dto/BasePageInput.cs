using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Dto
{
    public class BasePageInput
    {
        public BasePageInput()
        {
            this.Page = 1;
            this.PageSize = 20;
        }
        /// <summary>
        /// 当前分页
        /// </summary>
        public int Page { set; get; }
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { set; get; }
    }
}
