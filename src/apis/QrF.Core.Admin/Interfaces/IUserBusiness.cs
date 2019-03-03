using QrF.Core.Admin.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Interfaces
{
    public interface IUserBusiness
    {
        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<QueryUsersOutput> QueryUsers(QueryUsersInput input);
        /// <summary>
        /// 编辑用户信息
        /// </summary>
        Task EditUser(UserDto input);
    }
}
