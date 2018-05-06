using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.EntityFrameworkCore
{
    public interface IDbContextTypeMatcher
    {
        void Populate(Type[] dbContextTypes);

        Type GetConcreteType(Type sourceDbContextType);
    }
}
