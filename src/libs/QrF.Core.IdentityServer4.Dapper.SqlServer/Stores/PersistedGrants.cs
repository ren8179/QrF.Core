using Dapper;
using Microsoft.Extensions.Logging;
using QrF.Core.IdentityServer4.Dapper.Interfaces;
using QrF.Core.IdentityServer4.Dapper.Options;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace QrF.Core.IdentityServer4.Dapper.SqlServer.Stores
{
    public class PersistedGrants : IPersistedGrants
    {
        private readonly ILogger<PersistedGrants> _logger;
        private readonly DapperStoreOptions _configurationStoreOptions;

        public PersistedGrants(ILogger<PersistedGrants> logger, DapperStoreOptions configurationStoreOptions)
        {
            _logger = logger;
            _configurationStoreOptions = configurationStoreOptions;
        }

        /// <summary>
        /// 移除指定的时间过期授权信息
        /// </summary>
        /// <param name="dt">Utc时间</param>
        /// <returns></returns>
        public async Task RemoveExpireToken(DateTime dt)
        {
            using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            {
                string sql = "delete from PersistedGrants where Expiration>@dt";
                await connection.ExecuteAsync(sql, new { dt });
            }
        }
    }
}
