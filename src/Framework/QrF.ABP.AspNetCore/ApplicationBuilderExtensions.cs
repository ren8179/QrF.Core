using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using Castle.LoggingFacility.MsLogging;
using Microsoft.Extensions.Logging;
using QrF.ABP.AspNetCore.Security;

namespace QrF.ABP.AspNetCore
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseAbp(this IApplicationBuilder app)
        {
            app.UseAbp(null);
        }

        public static void UseAbp(this IApplicationBuilder app, Action<ApplicationBuilderOptions> optionsAction)
        {
            var options = new ApplicationBuilderOptions();
            optionsAction?.Invoke(options);

            if (options.UseCastleLoggerFactory)
            {
                app.UseCastleLoggerFactory();
            }

            InitializeAbp(app);
            
            if (options.UseSecurityHeaders)
            {
                app.UseAbpSecurityHeaders();
            }
        }
        

        private static void InitializeAbp(IApplicationBuilder app)
        {
            var abpBootstrapper = app.ApplicationServices.GetRequiredService<Bootstrapper>();
            abpBootstrapper.Initialize();

            var applicationLifetime = app.ApplicationServices.GetService<IApplicationLifetime>();
            applicationLifetime.ApplicationStopping.Register(() => abpBootstrapper.Dispose());
        }

        public static void UseCastleLoggerFactory(this IApplicationBuilder app)
        {
            var castleLoggerFactory = app.ApplicationServices.GetService<Castle.Core.Logging.ILoggerFactory>();
            if (castleLoggerFactory == null)
            {
                return;
            }

            app.ApplicationServices
                .GetRequiredService<ILoggerFactory>()
                .AddCastleLogger(castleLoggerFactory);
        }
        
        public static void UseAbpSecurityHeaders(this IApplicationBuilder app)
        {
            app.UseMiddleware<SecurityHeadersMiddleware>();
        }
    }
}
