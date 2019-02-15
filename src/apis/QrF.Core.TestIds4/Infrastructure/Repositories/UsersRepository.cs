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
        public Users FindUserByuAccount(string uaccount, string upassword)
        {
            using (var connection = new SqlConnection(DbConn))
            {
                string sql = @"SELECT * from Users where uAccount=@uaccount and uPassword=@upassword and uStatus=1";
                var result = connection.QueryFirstOrDefault<Users>(sql, new { uaccount, upassword=upassword.ToMd5() });
                return result;
            }
        }

        /// <summary>
        /// 根据用户主键获取用户实体
        /// </summary>
        /// <param name="sub">用户标识</param>
        /// <returns></returns>
        public Users FindUserByUid(string sub)
        {
            using (var connection = new SqlConnection(DbConn))
            {
                string sql = @"SELECT * from Users where uid=@uid";
                var result = connection.QueryFirstOrDefault<Users>(sql, new { uid = sub });
                return result;
            }
        }
    }
}
