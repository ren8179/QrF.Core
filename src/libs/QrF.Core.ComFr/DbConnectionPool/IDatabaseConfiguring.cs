using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace QrF.Core.ComFr.DbConnectionPool
{
    public interface IDatabaseConfiguring
    {
        void OnConfiguring(DbContextOptionsBuilder optionsBuilder, DbConnection dbConnectionForReusing);
    }
}
