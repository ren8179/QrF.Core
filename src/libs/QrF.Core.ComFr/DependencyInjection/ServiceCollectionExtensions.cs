using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.ComFr.DependencyInjection
{
   public static class ServiceCollectionExtensions
    {
        public static void UseComFr(this IServiceCollection services, IConfiguration configuration)
        {

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDataProtection();
        }
        
    }
}
