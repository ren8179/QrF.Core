using QrF.ABP.Dependency;
using QrF.ABP.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.EntityFrameworkCore.Repositories
{
    public interface IEfGenericRepositoryRegistrar
    {
        void RegisterForDbContext(Type dbContextType, IIocManager iocManager, AutoRepositoryTypesAttribute defaultAutoRepositoryTypesAttribute);
    }
}
