using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.EntityFrameworkCore
{
    public class EfCoreBasedSecondaryOrmRegistrar : SecondaryOrmRegistrarBase
    {
        public EfCoreBasedSecondaryOrmRegistrar(Type dbContextType, IDbContextEntityFinder dbContextEntityFinder)
            : base(dbContextType, dbContextEntityFinder)
        {
        }

        public override string OrmContextKey { get; } = "EntityFrameworkCore";
    }
}
