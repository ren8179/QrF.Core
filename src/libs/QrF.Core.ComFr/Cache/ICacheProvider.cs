using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.ComFr.Cache
{
    public interface ICacheProvider
    {
        ICacheManager<T> Build<T>();
    }
}
