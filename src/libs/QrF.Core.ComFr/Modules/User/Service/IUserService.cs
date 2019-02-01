using QrF.Core.ComFr.Constant;
using QrF.Core.ComFr.Modules.User.Models;
using QrF.Core.ComFr.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.ComFr.Modules.User.Service
{
    public interface IUserService : IService<UserEntity>
    {
        UserEntity Login(string userID, string passWord, UserType userType, string ip);
        UserEntity SetResetToken(string userID, UserType userType);
        bool ResetPassWord(string token, string newPassword);
    }
}
