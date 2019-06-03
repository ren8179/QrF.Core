using QrF.Core.Admin.Domain;
using QrF.Core.Admin.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Interfaces
{
    public interface IPermissionsBusiness
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        Task<IEnumerable<Permissions>> GetListByUserTypeAsync(Guid userId, int type = 2);
        /// <summary>
        /// 查询列表
        /// </summary>
        Task<IEnumerable<Permissions>> GetListByRoleTypeAsync(Guid roleId, int type = 1);
        /// <summary>
        /// 用户授权角色
        /// </summary>
        Task<bool> ToRole(ToRoleInput input);
        /// <summary>
        /// 角色授权
        /// </summary>
        Task<bool> SaveRoleMenu(RoleMenuDto input);
    }
}
