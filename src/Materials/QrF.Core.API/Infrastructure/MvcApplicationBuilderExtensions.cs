using Microsoft.AspNetCore.Builder;
using QrF.Core.API.Middleware;

namespace QrF.Core.API.Infrastructure
{
    public static class MvcApplicationBuilderExtensions
    {
        public static void UseCustomExceptionHandling(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(ExceptionHandlingMiddleware));

        public static void UseCustomLogging(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(LoggingMiddleware));
    }
}
