using QrF.Core.ComFr.Constant;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.ComFr.Options
{
    public class DatabaseOption
    {
        public DbTypes DbType { get; set; }
        public string ConnectionString { get; set; }
    }
}
