using Microsoft.EntityFrameworkCore;
using QrF.Core.ComFr.Constant;
using QrF.Core.ComFr.Modules.User.Models;
using QrF.Core.ComFr.Repositories;
using QrF.Core.Utils.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace QrF.Core.ComFr.Modules.User.Service
{
    public class UserService : ServiceBase<UserEntity, ComFrDbContext>, IUserService
    {
        public UserService(IApplicationContext applicationContext, ComFrDbContext dbContext) : base(applicationContext, dbContext)
        {
        }
        public override DbSet<UserEntity> CurrentDbSet
        {
            get
            {
                return DbContext.Users;
            }
        }
        public override UserEntity Get(params object[] primaryKey)
        {
            var userEntity = CurrentDbSet.AsNoTracking().Where(m => m.UserID == primaryKey[0].ToString()).FirstOrDefault();
            return userEntity;
        }
        public override IQueryable<UserEntity> Get()
        {
            return CurrentDbSet.AsNoTracking();
        }

        private string ProtectPassWord(string passWord)
        {
            if (!passWord.IsNullOrWhiteSpace())
            {
                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(passWord.ToByte());
                    return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }
            }
            return passWord;
        }
        public override ServiceResult<UserEntity> Add(UserEntity item)
        {
            if (item.UserID.IsNullOrEmpty() && !item.Email.IsNullOrWhiteSpace())
            {
                item.UserID = item.Email;
            }
            if (!item.PassWordNew.IsNullOrWhiteSpace())
            {
                item.PassWord = item.PassWordNew;
            }
            if (!item.PassWord.IsNullOrWhiteSpace())
            {
                item.PassWord = ProtectPassWord(item.PassWord);
            }
            if (!item.Status.HasValue)
            {
                item.Status = (int)RecordStatus.Active;
            }
            if (Get(item.UserID) != null)
            {
                throw new Exception($"用户 {item.UserID} 已存在");
            }
            if (!item.Email.IsNullOrWhiteSpace() && Count(m => m.Email == item.Email && m.UserTypeCD == item.UserTypeCD) > 0)
            {
                throw new Exception($"邮件地址 {item.Email} 已被使用");
            }
            var result = base.Add(item);
            if (!result.HasViolation)
            {
                DbContext.SaveChanges();
            }
            return result;
        }

        public override ServiceResult<UserEntity> Update(UserEntity item)
        {
            if (!item.PassWordNew.IsNullOrWhiteSpace())
            {
                item.PassWord = ProtectPassWord(item.PassWordNew);
            }
            if (!item.Email.IsNullOrWhiteSpace() && Count(m => m.UserID != item.UserID && m.Email == item.Email && m.UserTypeCD == item.UserTypeCD) > 0)
            {
                throw new Exception($"邮件地址 {item.Email} 已被使用");
            }

            var result = base.Update(item);
            return result;
        }

        public UserEntity Login(string userID, string passWord, UserType userType, string ip)
        {
            if (userID.IsNullOrWhiteSpace() || passWord.IsNullOrWhiteSpace()) return null;
            var result = Get(m => (m.UserID == userID || m.Email == userID) && m.UserTypeCD == (int)userType && m.Status == (int)RecordStatus.Active && m.PassWord == ProtectPassWord(passWord)).FirstOrDefault();
            if (result != null)
            {
                result.ApiLoginToken = (result.UserID + result.PassWord + DateTime.Now.ToShortDateString()).ToMd5().ToLower();
                result.LastLoginDate = DateTime.Now;
                result.LoginIP = ip;
                Update(result);
                return result;
            }
            return null;
        }

        public UserEntity SetResetToken(string userID, UserType userType)
        {
            var user = Get(m => (m.UserID == userID || m.Email == userID) && m.UserTypeCD == (int)userType).FirstOrDefault();
            if (user != null)
            {
                user.ResetToken = Guid.NewGuid().ToString("N");
                user.ResetTokenDate = DateTime.Now;
                Update(user);
            }
            return user;
        }

        public bool ResetPassWord(string token, string newPassword)
        {
            var user = Get(m => m.ResetToken == token && m.UserTypeCD == (int)UserType.Customer).FirstOrDefault();
            if (user != null)
            {
                if (user.ResetTokenDate.HasValue && (DateTime.Now - user.ResetTokenDate.Value).TotalHours < 24)
                {
                    user.ResetToken = null;
                    user.ResetTokenDate = null;
                    user.PassWordNew = newPassword;
                    Update(user);
                    return true;
                }
            }
            return false;
        }
    }
}
