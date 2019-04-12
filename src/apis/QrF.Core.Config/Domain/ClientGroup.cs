using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Config.Domain
{
    /// <summary>
    /// 客户端配置授权组表
    /// </summary>
    [SugarTable("ClientGroup")]
    public class ClientGroup
    {
        /// <summary>
        /// 客户端授权主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ClientGroupId { get; set; }
        /// <summary>
        /// 客户端主键
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 授权组主键
        /// </summary>
        public int? GroupId { get; set; }
    }
}
