using QrF.Core.Admin.Domain;
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
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BasePageQueryOutput<QueryUserDto>> GetPageList(QueryUsersInput input);
        /// <summary>
        /// 编辑信息
        /// </summary>
        Task EditModel(UserDto input);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DelModel(Guid input);
        /// <summary>
        /// 根据账号密码获取用户实体
        /// </summary>
        /// <param name="uaccount">账号</param>
        /// <param name="upassword">密码</param>
        /// <returns></returns>
        User FindUserByuAccount(string uaccount, string upassword);
        /// <summary>
        /// 获取用户实体
        /// </summary>
        User GetByKeyId(Guid keyId);
    }
}
