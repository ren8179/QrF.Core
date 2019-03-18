using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Domain
{
    [SugarTable("Sys_Role")]
    public class Role
    {
        /// <summary>
        /// 资源唯一标示符
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public Guid KeyId { get; set; }
        /// <summary>
        /// 归属部门
        /// </summary>
        public Guid DeptId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        public string Codes { get; set; }
        /// <summary>
        /// 是否为超级管理员
        /// </summary>           
        public bool IsSystem { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
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
