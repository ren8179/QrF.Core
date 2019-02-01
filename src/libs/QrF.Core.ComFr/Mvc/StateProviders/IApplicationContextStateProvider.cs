using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.ComFr.Mvc.StateProviders
{
    public interface IApplicationContextStateProvider
    {
        string Name { get; }
        Func<IApplicationContext, T> Get<T>();
    }
}
