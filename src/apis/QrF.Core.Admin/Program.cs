using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using QrF.Core.Utils.Web;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace QrF.Core.Admin
{
    public class Program
    {
        public static string IP;
        public static int Port;
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.') + 1);
        public static string BasePath;

        public static int Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var isService = !(Debugger.IsAttached || args.Contains("--console"));
            var bud = new ConfigurationBuilder();
            BasePath = AppContext.BaseDirectory;
            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                BasePath = Path.GetDirectoryName(pathToExe);
            }
            var config = GetConfiguration(BasePath);
            IP = config["IP"];
            int.TryParse(config["Port"], out Port);

            Log.Logger = CreateSerilogLogger(config);
            try
            {
                if (string.IsNullOrEmpty(IP)) IP = NetworkHelper.LocalIPAddress;
                if (Port == 0) Port = NetworkHelper.GetRandomAvaliablePort();
                Log.Debug("Configuring web host ({Application})...", AppName);
                var host = BuildWebHost(config, BasePath, args.Where(arg => arg != "--console").ToArray());
                Log.Logger.Information("Starting {Application}({version}) {Service} {url} ", AppName, config["Version"], isService ? "win service" : "web host", $"http://{IP}:{Port}/");
                if (isService)
                    host.RunAsCustomService();
                else
                    host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({Application})!", AppName);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        private static IWebHost BuildWebHost(IConfiguration configuration, string basepath, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(false)
                .UseStartup<Startup>()
                .UseUrls($"http://{IP}:{Port}")
                .UseContentRoot(basepath)
                .UseConfiguration(configuration)
                .UseSerilog()
                .Build();

        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .Enrich.WithProperty("Application", AppName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
        private static IConfiguration GetConfiguration(string basepath)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(basepath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}
