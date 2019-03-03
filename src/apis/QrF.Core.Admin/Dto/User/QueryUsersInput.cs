using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Dto
{
    public class QueryUsersInput : BasePageInput
    {
        /// <summary>
        /// 归属部门
        /// </summary>
        public Guid? DeptId { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? Status { get; set; }
    }
}
