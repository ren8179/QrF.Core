using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Dto
{
    public class QueryRoleDto
    {
        /// <summary>
        ///  
        /// </summary>
        public Guid KeyId { set; get; }
        /// <summary>
        /// 归属部门
        /// </summary>
        public Guid DeptId { get; set; }
        public string DeptName { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        public string Codes { get; set; }
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
