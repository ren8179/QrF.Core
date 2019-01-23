using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Xml;

namespace QrF.Core.Log4Net
{
    public class Log4NetProvider : ILoggerProvider
    {
        private const string DefaultLog4NetFileName = "log4net.config";
        private readonly ILoggerRepository loggerRepository;
        private readonly ConcurrentDictionary<string, Log4NetLogger> loggers = new ConcurrentDictionary<string, Log4NetLogger>();
        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetProvider"/> class.
        /// </summary>
        public Log4NetProvider() : this(DefaultLog4NetFileName) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetProvider"/> class.
        /// </summary>
        public Log4NetProvider(bool watch, string repository) : this(DefaultLog4NetFileName, watch, repository) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetProvider"/> class.
        /// </summary>
        /// <param name="log4NetConfigFile">The log4NetConfigFile.</param>
        public Log4NetProvider(string log4NetConfigFile) : this(log4NetConfigFile, false) { }
        /// <summary>
		/// Initializes a new instance of the <see cref="Log4NetProvider" /> class.
		/// </summary>
		/// <param name="log4NetConfigFile">The log4 net configuration file.</param>
		/// <param name="watch">if set to <c>true</c> [watch].</param>
		/// <exception cref="NotSupportedException">Watch cannot be true if you are overwriting config file values with values from configuration section.</exception>
		public Log4NetProvider(string log4NetConfigFile, bool watch, string repository = "")
        {
            if (!string.IsNullOrEmpty(repository))
            {
                try
                {
                    this.loggerRepository = LogManager.GetRepository(repository);
                }
                catch
                {
                    this.loggerRepository = LogManager.CreateRepository(repository, typeof(log4net.Repository.Hierarchy.Hierarchy));
                }
            }
            else
            {
                var assembly = Assembly.GetExecutingAssembly();
                this.loggerRepository = LogManager.CreateRepository(
                    assembly ?? GetCallingAssemblyFromStartup(),
                    typeof(log4net.Repository.Hierarchy.Hierarchy));
            }
            if (watch)
            {
                XmlConfigurator.ConfigureAndWatch(this.loggerRepository, new FileInfo(Path.GetFullPath(log4NetConfigFile)));
            }
            else
            {
                var configXml = ParseLog4NetConfigFile(log4NetConfigFile);
                XmlConfigurator.Configure(this.loggerRepository, configXml);
            }
        }

        /// <summary>
        /// Creates the logger.
        /// </summary>
        /// <returns>An instance of the <see cref="ILogger"/>.</returns>
        public ILogger CreateLogger()
            => this.CreateLogger(this.loggerRepository.Name);

        public ILogger CreateLogger(string categoryName)
                => this.loggers.GetOrAdd(categoryName, this.CreateLoggerImplementation);
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                return;
            }

            this.loggers.Clear();
        }
        /// <summary>
        /// Parses log4net config file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>The <see cref="XmlElement"/> with the log4net XML element.</returns>
        private static XmlElement ParseLog4NetConfigFile(string filename)
        {
            using (FileStream fp = File.OpenRead(filename))
            {
                var settings = new XmlReaderSettings
                {
                    DtdProcessing = DtdProcessing.Prohibit
                };

                var log4netConfig = new XmlDocument();
                using (var reader = XmlReader.Create(fp, settings))
                {
                    log4netConfig.Load(reader);
                }
                if (log4netConfig.SelectSingleNode("/configuration/log4net") is XmlElement result)
                    return result;
                return log4netConfig.DocumentElement;
            }
        }

        /// <summary>
        /// Tries to retrieve the assembly from a "Startup" type found in the stack trace.
        /// </summary>
        /// <returns>Null for NetCoreApp 1.1, otherwise, Assembly of Startup type if found in stack trace.</returns>
        private static Assembly GetCallingAssemblyFromStartup()
        {
            var stackTrace = new System.Diagnostics.StackTrace(2);

            for (int i = 0; i < stackTrace.FrameCount; i++)
            {
                var frame = stackTrace.GetFrame(i);
                var type = frame.GetMethod()?.DeclaringType;

                if (string.Equals(type?.Name, "Startup", StringComparison.OrdinalIgnoreCase))
                {
                    return type.Assembly;
                }
            }

            return null;
        }

        private Log4NetLogger CreateLoggerImplementation(string name)
            => new Log4NetLogger(this.loggerRepository, name, false);
    }
}
