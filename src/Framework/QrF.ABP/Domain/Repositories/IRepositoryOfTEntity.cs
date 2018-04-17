using QrF.ABP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Domain.Repositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity<int>
    {

    }
}
