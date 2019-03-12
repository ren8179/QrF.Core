using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Domain
{
    [SugarTable("Sys_Organize")]
    public class Organize
    {
        /// <summary>
        /// 资源唯一标示符
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public Guid KeyId { get; set; }
        /// <summary>
        /// 组织名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string BizCode { get; set; }
        /// <summary>
        /// 排序
        /// </summary>           
        public int Sort { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? CreateId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
