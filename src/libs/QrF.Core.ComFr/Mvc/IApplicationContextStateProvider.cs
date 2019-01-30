using System;

namespace QrF.Core.ComFr.Mvc
{
    public interface IApplicationContextStateProvider
    {
        string Name { get; }
        Func<IApplicationContext, T> Get<T>();
    }
}
