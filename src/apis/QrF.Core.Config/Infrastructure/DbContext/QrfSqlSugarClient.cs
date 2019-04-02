using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Config.Infrastructure.DbContext
{
    public class QrfSqlSugarClient : SqlSugarClient
    {
        public QrfSqlSugarClient(ConnectionConfig config) : base(config) { DbName = string.Empty; Default = true; }
        public QrfSqlSugarClient(ConnectionConfig config, string dbName) : base(config) { DbName = dbName; Default = true; }
        public QrfSqlSugarClient(ConnectionConfig config, string dbName, bool isDefault) : base(config) { DbName = dbName; Default = isDefault; }
        public string DbName { set; get; }
        public bool Default { set; get; }
    }
}
