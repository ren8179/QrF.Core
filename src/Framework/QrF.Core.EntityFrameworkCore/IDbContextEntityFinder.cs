using QrF.ABP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.EntityFrameworkCore
{
    public interface IDbContextEntityFinder
    {
        IEnumerable<EntityTypeInfo> GetEntityTypeInfos(Type dbContextType);
    }
}
