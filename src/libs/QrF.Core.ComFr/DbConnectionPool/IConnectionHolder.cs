using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace QrF.Core.ComFr.DbConnectionPool
{
    public interface IConnectionHolder
    {
        DbConnection DbConnection { get; }
    }
}
