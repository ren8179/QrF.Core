using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using QrF.Core.ComFr.Entities;
using QrF.Core.ComFr.Mvc.StateProviders;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace QrF.Core.ComFr.Mvc
{
    public class ApplicationContext : IApplicationContext
    {
        private readonly ConcurrentDictionary<string, Func<object>> _stateResolvers;
        private IEnumerable<IApplicationContextStateProvider> _applicationContextStateProviders;
        public ApplicationContext(IHttpContextAccessor httpContextAccessor)
        {
            _stateResolvers = new ConcurrentDictionary<string, Func<object>>();
            HttpContextAccessor = httpContextAccessor;
        }
        public IHttpContextAccessor HttpContextAccessor
        {
            get;
        }
        public IUser CurrentUser
        {
            get
            {
                return Get<IUser>(nameof(CurrentUser));
            }
        }
        public IUser CurrentCustomer
        {
            get
            {
                return Get<IUser>(nameof(CurrentCustomer));
            }
        }
        public IHostingEnvironment HostingEnvironment
        {
            get { return Get<IHostingEnvironment>(nameof(HostingEnvironment)); }
        }
        public bool IsAuthenticated
        {
            get { return HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated; }
        }
        public T As<T>() where T : class, IApplicationContext
        {
            return this as T;
        }
        Func<object> FindResolverForState<T>(string name)
        {
            if (_applicationContextStateProviders == null)
            {
                _applicationContextStateProviders = HttpContextAccessor.HttpContext.RequestServices.GetServices<IApplicationContextStateProvider>();
            }
            var resolver = _applicationContextStateProviders.FirstOrDefault(m => m.Name == name).Get<T>();

            if (resolver == null)
            {
                return () => default(T);
            }

            return () => resolver(this);
        }
        public T Get<T>(string name)
        {
            var provider = _stateResolvers.GetOrAdd(name, key => FindResolverForState<T>(key));
            if (provider != null)
            {
                return (T)provider();
            }
            return default(T);
        }

        public void Set(string name, object value)
        {
            if (_stateResolvers.ContainsKey(name))
            {
                _stateResolvers[name] = () => value;
            }
            else
            {
                _stateResolvers.TryAdd(name, () => value);
            }
        }
    }
}
