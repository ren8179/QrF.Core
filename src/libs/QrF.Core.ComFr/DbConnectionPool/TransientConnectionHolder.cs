﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace QrF.Core.ComFr.DbConnectionPool
{
    public class TransientConnectionHolder : IDisposable, IConnectionHolder
    {
        private readonly IDbConnectionPool _pool;
        private readonly bool _objectShouldReturnToPool;

        public TransientConnectionHolder(IDbConnectionPool pool)
        {
            _pool = pool;
            DbConnection = _pool.ConnectionPool?.Get();
            if (DbConnection != null)
            {
                _objectShouldReturnToPool = true;
            }
            else
            {
                DbConnection = _pool.CreateDbConnection();
                _objectShouldReturnToPool = false;
            }
            if (DbConnection != null && DbConnection.State == System.Data.ConnectionState.Closed)
            {//预先打开数据库，为的是确保在使用EF的场合（比如ASP.Net）不会频繁打开/关闭数据库而致使性能下降
                DbConnection.Open();
            }
        }
        public DbConnection DbConnection { get; private set; }
        public void Dispose()
        {
            if (DbConnection != null)
            {
                if (_objectShouldReturnToPool)
                {
                    _pool.ConnectionPool.Return(DbConnection);
                }
                else
                {
                    DbConnection.Dispose();
                }

                DbConnection = null;
            }
        }
    }
}
