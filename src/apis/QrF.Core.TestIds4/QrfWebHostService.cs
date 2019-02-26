using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ServiceProcess;

namespace QrF.Core.TestIds4
{
    internal class QrfWebHostService : WebHostService
    {
        private ILogger _logger;

        public QrfWebHostService(IWebHost host) : base(host)
        {
            _logger = host.Services.GetRequiredService<ILogger<QrfWebHostService>>();
        }

        protected override void OnStarting(string[] args)
        {
            base.OnStarting(args);
        }

        protected override void OnStarted()
        {
            _logger.LogInformation($"服务已启动");
            base.OnStarted();
        }

        protected override void OnStopping()
        {
            _logger.LogDebug("服务已停止");
            base.OnStopping();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public static class WebHostServiceExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static void RunAsCustomService(this IWebHost host)
        {
            var webHostService = new QrfWebHostService(host);
            ServiceBase.Run(webHostService);
        }
    }
}
