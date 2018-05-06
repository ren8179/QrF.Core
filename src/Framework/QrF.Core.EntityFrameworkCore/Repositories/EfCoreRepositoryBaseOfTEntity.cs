using Microsoft.EntityFrameworkCore;
using QrF.ABP.Domain.Entities;
using QrF.ABP.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.EntityFrameworkCore.Repositories
{
    public class EfCoreRepositoryBase<TDbContext, TEntity> : EfCoreRepositoryBase<TDbContext, TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
        where TDbContext : DbContext
    {
        public EfCoreRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
