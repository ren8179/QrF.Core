using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using QrF.ABP.AspNetCore.Mvc.Conventions;
using QrF.ABP.AspNetCore.Mvc.ExceptionHandling;
using QrF.ABP.AspNetCore.Mvc.ModelBinding;
using QrF.ABP.AspNetCore.Mvc.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.AspNetCore.Mvc
{
    internal static class MvcOptionsExtensions
    {
        public static void AddAbp(this MvcOptions options, IServiceCollection services)
        {
            AddConventions(options, services);
            AddFilters(options);
            AddModelBinders(options);
        }

        private static void AddConventions(MvcOptions options, IServiceCollection services)
        {
            options.Conventions.Add(new AppServiceConvention(services));
        }

        private static void AddFilters(MvcOptions options)
        {
            options.Filters.AddService(typeof(ExceptionFilter));
            options.Filters.AddService(typeof(ResultFilter));
        }

        private static void AddModelBinders(MvcOptions options)
        {
            options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
        }
    }
}
