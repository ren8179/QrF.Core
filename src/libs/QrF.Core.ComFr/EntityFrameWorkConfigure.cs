using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using QrF.Core.ComFr.Constant;
using QrF.Core.ComFr.DbConnectionPool;
using QrF.Core.ComFr.Options;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace QrF.Core.ComFr
{
    public class EntityFrameWorkConfigure : IDatabaseConfiguring
    {
        private readonly DatabaseOption _dataBaseOption;
        private readonly ILoggerFactory _loggerFactory;
        public EntityFrameWorkConfigure(DatabaseOption dataBaseOption, ILoggerFactory loggerFactory)
        {
            _dataBaseOption = dataBaseOption;
            _loggerFactory = loggerFactory;
        }
        public void OnConfiguring(DbContextOptionsBuilder optionsBuilder, DbConnection dbConnectionForReusing)
        {
            switch (_dataBaseOption.DbType)
            {
                case DbTypes.MsSql:
                    {
                        if (dbConnectionForReusing != null)
                            optionsBuilder.UseSqlServer(dbConnectionForReusing);
                        else
                            optionsBuilder.UseSqlServer(_dataBaseOption.ConnectionString);
                        break;
                    }
                case DbTypes.MsSqlEarly:
                    {
                        if (dbConnectionForReusing != null)
                            optionsBuilder.UseSqlServer(dbConnectionForReusing, option => option.UseRowNumberForPaging());
                        else
                            optionsBuilder.UseSqlServer(_dataBaseOption.ConnectionString, option => option.UseRowNumberForPaging());
                        break;
                    }
                //case DbTypes.Sqlite:
                //    {
                //        if (dbConnectionForReusing != null)
                //            optionsBuilder.UseSqlite(dbConnectionForReusing);
                //        else
                //            optionsBuilder.UseSqlite(_dataBaseOption.ConnectionString);
                //        break;
                //    }
                //case DbTypes.MySql:
                //    {
                //        if (dbConnectionForReusing != null)
                //            optionsBuilder.UseMySql(dbConnectionForReusing);
                //        else
                //            optionsBuilder.UseMySql(_dataBaseOption.ConnectionString);
                //        break;
                //    }
            }

            optionsBuilder.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }
    }
}
