using Microsoft.AspNetCore.Http;
using QrF.Core.ComFr.Entities;
using QrF.Core.ComFr.Modules.User.Service;
using QrF.Core.Utils.Extension;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace QrF.Core.ComFr.Mvc.StateProviders
{
    public class CurrentUserStateProvider : IApplicationContextStateProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserStateProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string Name => "CurrentUser";
        IUser _currentUser;
        public Func<IApplicationContext, T> Get<T>()
        {
            return context =>
            {
                if (_currentUser != null)
                {
                    return (T)_currentUser;
                }
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext != null && httpContext.User.Identity.IsAuthenticated && !httpContext.User.Identity.Name.IsNullOrWhiteSpace())
                {
                    using (var userService = httpContext.RequestServices.GetService<IUserService>())
                    {
                        _currentUser = userService.Get(httpContext.User.Identity.Name);
                        return (T)_currentUser;
                    }

                }
                return default(T);
            };
        }
    }
}
