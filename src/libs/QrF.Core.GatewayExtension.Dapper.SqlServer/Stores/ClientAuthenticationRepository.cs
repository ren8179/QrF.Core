using Dapper;
using QrF.Core.GatewayExtension.Authentication;
using QrF.Core.GatewayExtension.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace QrF.Core.GatewayExtension.Dapper.SqlServer.Stores
{
    /// <summary>
    /// 使用sqlserver实现客户端授权仓储
    /// </summary>
    public class ClientAuthenticationRepository : IClientAuthenticationRepository
    {
        private readonly CusOcelotConfiguration _option;
        public ClientAuthenticationRepository(CusOcelotConfiguration option)
        {
            _option = option;
        }
        /// <summary>
        /// 校验获取客户端是否有访问权限
        /// </summary>
        /// <param name="clientid">客户端ID</param>
        /// <param name="path">请求路由</param>
        /// <returns></returns>
        public async Task<bool> ClientAuthenticationAsync(string clientid, string path)
        {
            using (var connection = new SqlConnection(_option.DbConnectionStrings))
            {
                string sql = @"SELECT COUNT(1) FROM  Clients T1 INNER JOIN ClientGroup T2 ON T1.Id=T2.Id INNER JOIN AuthGroup T3 ON T2.GroupId = T3.GroupId INNER JOIN ReRouteGroupAuth T4 ON T3.GroupId = T4.GroupId INNER JOIN ReRoute T5 ON T4.ReRouteId = T5.ReRouteId WHERE Enabled = 1 AND ClientId = @ClientId AND T5.InfoStatus = 1 AND UpstreamPathTemplate = @Path";
                var result = await connection.QueryFirstOrDefaultAsync<int>(sql, new { ClientId = clientid, Path = path });
                return result > 0;
            }
        }
    }
}
