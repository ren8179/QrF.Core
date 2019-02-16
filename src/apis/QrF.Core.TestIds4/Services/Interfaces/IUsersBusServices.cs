using QrF.Core.Storage.Entities;

namespace QrF.Core.TestIds4.Services.Interfaces
{
    public interface IUsersBusServices
    {
        /// <summary>
        /// 根据账号密码获取用户实体
        /// </summary>
        /// <param name="uaccount">账号</param>
        /// <param name="upassword">密码</param>
        /// <returns></returns>
        SysUser FindUserByuAccount(string uaccount, string upassword);

        /// <summary>
        /// 根据用户主键获取用户实体
        /// </summary>
        /// <param name="sub">用户标识</param>
        /// <returns></returns>
        SysUser FindUserByUid(string sub);
    }
}
