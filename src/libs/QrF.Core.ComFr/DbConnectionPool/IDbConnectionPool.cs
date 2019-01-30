using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace QrF.Core.ComFr.DbConnectionPool
{
    public interface IDbConnectionPool
    {
        ObjectPool<DbConnection> ConnectionPool { get; set; }
        DbConnection CreateDbConnection();
    }
}
