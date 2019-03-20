using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Dto
{
    public class ToRoleInput 
    {
        /// <summary>
        /// 角色ID
        /// </summary>           
        public Guid RoleId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>           
        public Guid? MenuId { get; set; }

        /// <summary>
        /// 按钮功能编号
        /// </summary>
        public Guid? BtnFunId { get; set; }

        /// <summary>
        /// 授权类型1=角色-菜单 2=用户-角色 3=角色-菜单-按钮功能
        /// 默认=1
        /// </summary>
        public int Types { get; set; } = 1;
        /// <summary>
        /// 是否授权
        /// </summary>
        public bool Status { get; set; }
    }
}
