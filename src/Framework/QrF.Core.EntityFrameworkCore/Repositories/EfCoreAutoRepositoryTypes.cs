using QrF.ABP.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.EntityFrameworkCore.Repositories
{
    public static class EfCoreAutoRepositoryTypes
    {
        public static AutoRepositoryTypesAttribute Default { get; }

        static EfCoreAutoRepositoryTypes()
        {
            Default = new AutoRepositoryTypesAttribute(
                typeof(IRepository<>),
                typeof(IRepository<,>),
                typeof(EfCoreRepositoryBase<,>),
                typeof(EfCoreRepositoryBase<,,>)
            );
        }
    }
}
