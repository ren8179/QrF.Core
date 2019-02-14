using QrF.Core.Storage.Entities;
using QrF.Core.TestIds4.Infrastructure.Interfaces;
using QrF.Core.TestIds4.Services.Interfaces;

namespace QrF.Core.TestIds4.Services
{
    public class UsersBusServices : IUsersBusServices
    {
        private readonly IUsersRepository _repository;
        public UsersBusServices(IUsersRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 根据账号密码获取用户实体
        /// </summary>
        /// <param name="uaccount">账号</param>
        /// <param name="upassword">密码</param>
        /// <returns></returns>
        public Users FindUserByuAccount(string uaccount, string upassword)
        {
            return _repository.FindUserByuAccount(uaccount, upassword);
        }

        /// <summary>
        /// 根据用户主键获取用户实体
        /// </summary>
        /// <param name="sub">用户标识</param>
        /// <returns></returns>
        public Users FindUserByUid(string sub)
        {
            return _repository.FindUserByUid(sub);
        }
    }
}
