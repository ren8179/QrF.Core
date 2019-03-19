using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Dto
{
    public class QueryLogsInput : BasePageInput
    {
        /// <summary>
        /// 级别
        /// </summary>
        public string Level { get; set; }
    }
}
