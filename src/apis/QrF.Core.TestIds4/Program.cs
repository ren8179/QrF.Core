using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using QrF.Core.Log4Net;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace QrF.Core.TestIds4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);  //注册编码提供程序
            var isService = !(Debugger.IsAttached || args.Contains("--console"));
            var bud = new ConfigurationBuilder();
            var basepath = AppContext.BaseDirectory;
            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                basepath = Path.GetDirectoryName(pathToExe);
            }
            bud.SetBasePath(basepath);
            var config = bud.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();
            var builder = CreateWebHostBuilder(config, basepath, args.Where(arg => arg != "--console").ToArray());
            if (isService) builder.UseContentRoot(basepath);
            var host = builder.Build();
            if (isService)
            {
                host.RunAsCustomService();
            }
            else
            {
                host.Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(IConfigurationRoot config, string basePath, string[] args)
        {
            var ip = config["IP"];
            var port = config["Port"];
            if (string.IsNullOrEmpty(ip)) ip = "*";
            if (string.IsNullOrEmpty(port)) port = "5000";
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls($"http://{ip}:{port}")
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddLog4Net(basePath + "/log4net.config", true, config["Name"]);
                })
                .ConfigureAppConfiguration((hostingContext, builder) =>
                {
                    //builder.AddJsonFile("configuration.json", false, true);
                    builder.AddJsonFile("appsettings.json", false, true);
                });
        }
    }
}
