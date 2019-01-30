﻿using Microsoft.Extensions.ObjectPool;
using QrF.Core.ComFr.Constant;
using QrF.Core.ComFr.Options;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace QrF.Core.ComFr.DbConnectionPool
{
    /// <summary>
    /// 仅仅对单独一个数据库提供的连接池，对多个数据库的情形不支持
    /// </summary>
    public class SimpleDbConnectionPool : IDisposable, IDbConnectionPool
    {
        private class PooledObjectPolicy : PooledObjectPolicy<DbConnection>
        {
            internal PooledObjectPolicy(int maximumRetained, Func<DbConnection> funcCreateConnection)
            {
                _maximumRetained = maximumRetained;
                _createdObjects = new DbConnection[_maximumRetained];
                _funcCreateConnection = funcCreateConnection;
            }
            internal readonly DbConnection[] _createdObjects;
            private readonly int _maximumRetained;
            private readonly Func<DbConnection> _funcCreateConnection;
            private int _numObjectsInPool;

            public override DbConnection Create()
            {
                int cnt = System.Threading.Interlocked.Increment(ref _numObjectsInPool);
                if (cnt > _maximumRetained)
                {
                    System.Threading.Interlocked.Decrement(ref _numObjectsInPool);
                    return null;
                }
                DbConnection dbConnection = _funcCreateConnection();
                _createdObjects[cnt - 1] = dbConnection;
                return dbConnection;
            }
            public override bool Return(DbConnection obj)
            {
                return true;
            }
        }

        private readonly PooledObjectPolicy _pooledObjectPolicy;

        public SimpleDbConnectionPool(Options options, DatabaseOption databaseOption)
        {
            DatabaseOption = databaseOption;
            int maximumRetained = Math.Max(options.MaximumRetained, 0);
            if (maximumRetained > 0)
            {
                _pooledObjectPolicy = new PooledObjectPolicy(maximumRetained, CreateDbConnection);
                ConnectionPool = new DefaultObjectPool<DbConnection>(_pooledObjectPolicy, maximumRetained);
            }
        }

        public ObjectPool<DbConnection> ConnectionPool { get; set; }

        public DatabaseOption DatabaseOption { get; set; }
        public virtual DbConnection CreateDbConnection()
        {
            switch (DatabaseOption.DbType)
            {
                case DbTypes.MsSql:
                case DbTypes.MsSqlEarly:
                    return null;
                case DbTypes.Sqlite:
                    return null;
                case DbTypes.MySql:
                    return null;
            }
            return null;
        }
        public void Dispose()
        {
            if (_pooledObjectPolicy != null)
            {
                DbConnection[] objects = _pooledObjectPolicy._createdObjects;
                for (int i = 0; i < objects.Length; ++i)
                {
                    if (objects[i] != null)
                    {
                        objects[i].Dispose();
                        objects[i] = null;
                    }
                }
            }
        }

    }
}
