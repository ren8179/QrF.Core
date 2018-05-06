using QrF.ABP.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Domain.Repositories
{
    public interface ISecondaryOrmRegistrar
    {
        string OrmContextKey { get; }

        void RegisterRepositories(IIocManager iocManager, AutoRepositoryTypesAttribute defaultRepositoryTypes);
    }
}
