using Dapper;
using Microsoft.Extensions.Options;
using QrF.Core.Storage.Entities;
using QrF.Core.TestIds4.Infrastructure.Config;
using QrF.Core.TestIds4.Infrastructure.Interfaces;
using QrF.Core.Utils.Extension;
using System.Data.SqlClient;

namespace QrF.Core.TestIds4.Infrastructure.Repositories
{
    public class UsersRepository: IUsersRepository
    {
        private readonly string DbConn = "";
        public UsersRepository(IOptions<ApiOptions> options)
        {
            DbConn = options.Value.DbConnectionStrings;
        }

        /// <summary>
        /// 根据账号密码获取用户实体
        /// </summary>
        /// <param name="uaccount">账号</param>
        /// <param name="upassword">密码</param>
        /// <returns></returns>
        public SysUser FindUserByuAccount(string uaccount, string upassword)
        {
            using (var connection = new SqlConnection(DbConn))
            {
                string sql = @"SELECT * from Sys_User where Account=@uaccount and Password=@upassword and Status=1";
                var result = connection.QueryFirstOrDefault<SysUser>(sql, new { uaccount, upassword=upassword.ToMd5() });
                return result;
            }
        }

        /// <summary>
        /// 根据用户主键获取用户实体
        /// </summary>
        /// <param name="sub">用户标识</param>
        /// <returns></returns>
        public SysUser FindUserByUid(string sub)
        {
            using (var connection = new SqlConnection(DbConn))
            {
                string sql = @"SELECT * from Sys_User where KeyId=@uid";
                var result = connection.QueryFirstOrDefault<SysUser>(sql, new { uid = sub });
                return result;
            }
        }
    }
}
