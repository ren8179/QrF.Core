﻿using Microsoft.EntityFrameworkCore;
using QrF.ABP.Data;
using QrF.ABP.Domain.Entities;
using QrF.ABP.Domain.Repositories;
using QrF.ABP.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QrF.Core.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Implements IRepository for Entity Framework.
    /// </summary>
    /// <typeparam name="TDbContext">DbContext which contains <typeparamref name="TEntity"/>.</typeparam>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity</typeparam>
    public class EfCoreRepositoryBase<TDbContext, TEntity, TPrimaryKey> :
        RepositoryBase<TEntity, TPrimaryKey>,
        ISupportsExplicitLoading<TEntity, TPrimaryKey>,
        IRepositoryWithDbContext

        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : DbContext
    {
        /// <summary>
        /// Gets EF DbContext object.
        /// </summary>
        public virtual TDbContext Context => _dbContextProvider.GetDbContext();

        /// <summary>
        /// Gets DbSet for given entity.
        /// </summary>
        public virtual DbSet<TEntity> Table => Context.Set<TEntity>();

        public virtual DbTransaction Transaction
        {
            get
            {
                return (DbTransaction)TransactionProvider?.GetActiveTransaction(new ActiveTransactionProviderArgs
                {
                    {"ContextType", typeof(TDbContext) }
                });
            }
        }

        public virtual DbConnection Connection
        {
            get
            {
                var connection = Context.Database.GetDbConnection();

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                return connection;
            }
        }

        public IActiveTransactionProvider TransactionProvider { private get; set; }

        private readonly IDbContextProvider<TDbContext> _dbContextProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public EfCoreRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public override IQueryable<TEntity> GetAll()
        {
            return GetAllIncluding();
        }

        public override IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = Table.AsQueryable();

            if (!propertySelectors.IsNullOrEmpty())
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return query;
        }

        public override async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public override async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public override async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().SingleAsync(predicate);
        }

        public override async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return await GetAll().FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        public override async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }

        public override TEntity Insert(TEntity entity)
        {
            return Table.Add(entity).Entity;
        }

        public override Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public override TPrimaryKey InsertAndGetId(TEntity entity)
        {
            entity = Insert(entity);

            if (entity.IsTransient())
            {
                Context.SaveChanges();
            }

            return entity.Id;
        }

        public override async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            entity = await InsertAsync(entity);

            if (entity.IsTransient())
            {
                await Context.SaveChangesAsync();
            }

            return entity.Id;
        }

        public override TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
        {
            entity = InsertOrUpdate(entity);

            if (entity.IsTransient())
            {
                Context.SaveChanges();
            }

            return entity.Id;
        }

        public override async Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            entity = await InsertOrUpdateAsync(entity);

            if (entity.IsTransient())
            {
                await Context.SaveChangesAsync();
            }

            return entity.Id;
        }

        public override TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public override Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity = Update(entity);
            return Task.FromResult(entity);
        }

        public override void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
        }

        public override void Delete(TPrimaryKey id)
        {
            var entity = GetFromChangeTrackerOrNull(id);
            if (entity != null)
            {
                Delete(entity);
                return;
            }

            entity = FirstOrDefault(id);
            if (entity != null)
            {
                Delete(entity);
                return;
            }

            //Could not found the entity, do nothing.
        }

        public override async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }

        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).CountAsync();
        }

        public override async Task<long> LongCountAsync()
        {
            return await GetAll().LongCountAsync();
        }

        public override async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).LongCountAsync();
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = Context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }

        public DbContext GetDbContext()
        {
            return Context;
        }

        public Task EnsureCollectionLoadedAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, IEnumerable<TProperty>>> collectionExpression,
            CancellationToken cancellationToken)
            where TProperty : class
        {
            return Context.Entry(entity).Collection(collectionExpression).LoadAsync(cancellationToken);
        }

        public Task EnsurePropertyLoadedAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, TProperty>> propertyExpression,
            CancellationToken cancellationToken)
            where TProperty : class
        {
            return Context.Entry(entity).Reference(propertyExpression).LoadAsync(cancellationToken);
        }

        private TEntity GetFromChangeTrackerOrNull(TPrimaryKey id)
        {
            var entry = Context.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<TPrimaryKey>.Default.Equals(id, (ent.Entity as TEntity).Id)
                );

            return entry?.Entity as TEntity;
        }
    }
}
