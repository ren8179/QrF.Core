using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace QrF.Core.ComFr
{
    public static class ServiceLocator
    {
        private static IHttpContextAccessor HttpContextAccessor;
        public static void Setup(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }
        public static T GetService<T>()
        {
            return HttpContextAccessor.HttpContext.RequestServices.GetService<T>();
        }
        public static IEnumerable<T> GetServices<T>()
        {
            return HttpContextAccessor.HttpContext.RequestServices.GetServices<T>();
        }
        public static object GetService(Type type)
        {
            return HttpContextAccessor.HttpContext.RequestServices.GetService(type);
        }
        public static IEnumerable<object> GetServices(Type type)
        {
            return HttpContextAccessor.HttpContext.RequestServices.GetServices(type);
        }
    }
}
