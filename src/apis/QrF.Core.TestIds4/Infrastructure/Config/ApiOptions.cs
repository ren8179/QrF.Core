using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.TestIds4.Infrastructure.Config
{
    public class ApiOptions
    {
        /// <summary>
        /// 网关授权地址
        /// </summary>
        public string PublicOrigin { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string DbConnectionStrings { get; set; }
    }
}
