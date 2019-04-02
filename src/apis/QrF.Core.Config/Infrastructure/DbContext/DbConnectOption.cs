using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Config.Infrastructure.DbContext
{
    public class DbConnectOption
    {
        public string Name { set; get; }
        public string ConnectionString { set; get; }
        public SqlSugar.DbType DbType { set; get; }
        public bool IsAutoCloseConnection { set; get; }
        public bool Default { set; get; } = true;
    }
}
