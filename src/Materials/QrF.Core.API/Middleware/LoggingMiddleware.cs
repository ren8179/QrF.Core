using Microsoft.AspNetCore.Http;
using NLog;
using System.Diagnostics;
using System.Threading.Tasks;

namespace QrF.Core.API.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            await _next(context);
            sw.Stop();
            LogManager.GetCurrentClassLogger().Info($@"HTTP {context.Request.Method} {context.Request.Path} responded {context.Response.StatusCode} in {sw.Elapsed.TotalMilliseconds} ms.");
        }
    }
}
