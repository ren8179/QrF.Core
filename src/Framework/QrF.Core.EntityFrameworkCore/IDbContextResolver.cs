using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace QrF.Core.EntityFrameworkCore
{
    public interface IDbContextResolver
    {
        TDbContext Resolve<TDbContext>(string connectionString, DbConnection existingConnection)
            where TDbContext : DbContext;
    }
}
