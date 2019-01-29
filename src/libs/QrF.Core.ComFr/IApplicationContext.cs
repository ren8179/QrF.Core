using Microsoft.AspNetCore.Hosting;
using QrF.Core.ComFr.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.ComFr
{
    public interface IApplicationContext
    {
        IUser CurrentUser { get; }
        IUser CurrentCustomer { get; }
        IHostingEnvironment HostingEnvironment { get; }
        bool IsAuthenticated { get; }
        T As<T>() where T : class, IApplicationContext;
        T Get<T>(string name);
        void Set(string name, object value);
    }
}
