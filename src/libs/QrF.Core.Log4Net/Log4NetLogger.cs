using log4net;
using log4net.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace QrF.Core.Log4Net
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _logger;
        public bool EnableScopes { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetLogger"/> class.
        /// </summary>
        public Log4NetLogger(ILoggerRepository loggerRepository, string categoryName, bool enableScopes)
        {
            _logger = LogManager.GetLogger(loggerRepository.Name, categoryName);
            EnableScopes = enableScopes;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
            => this._logger.Logger.Name;

        /// <summary>
        /// Begins a logical operation scope.
        /// </summary>
        /// <typeparam name="TState">The type of the state.</typeparam>
        /// <param name="state">The identifier for the scope.</param>
        /// <returns>
        /// An IDisposable that ends the logical operation scope on dispose.
        /// </returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));

            return Log4NetScope.Push(Name, state);
        }

        /// <summary>
        /// Determines whether the logging level is enabled.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <returns>The <see cref="bool"/> value indicating whether the logging level is enabled.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="logLevel"/> is outside allowed range</exception>
        public bool IsEnabled(LogLevel logLevel)
        {
            return LoggerEnagled(logLevel);
        }

        /// <summary>
        /// Logs an exception into the log.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="eventId">The event Id.</param>
        /// <param name="state">The state.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="formatter">The formatter.</param>
        /// <typeparam name="TState">The type of the state.</typeparam>
        /// <exception cref="ArgumentNullException">Throws when the <paramref name="formatter"/> is null.</exception>
        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!LoggerEnagled(logLevel))
            {
                return;
            }

            StringBuilder messageBuilder = new StringBuilder();
            if (formatter != null)
            {
                messageBuilder.Append(formatter(state, exception));
            }
            else
            {
                messageBuilder.Append(state);
            }

            if (EnableScopes)
            {
                AppendScopeInformation(messageBuilder);
            }

            GetLoggerAct(logLevel)(messageBuilder.ToString(), exception);
        }
        private bool LoggerEnagled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                    return _logger.IsDebugEnabled;
                case LogLevel.Information:
                    return _logger.IsInfoEnabled;
                case LogLevel.Warning:
                    return _logger.IsWarnEnabled;
                case LogLevel.Error:
                    return _logger.IsErrorEnabled;
                case LogLevel.Critical:
                    return _logger.IsFatalEnabled;
                case LogLevel.None:
                default:
                    return false;
            }
        }
        private Action<string, Exception> GetLoggerAct(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                    return _logger.Debug;
                case LogLevel.Information:
                    return _logger.Info;
                case LogLevel.Warning:
                    return _logger.Warn;
                case LogLevel.Error:
                    return _logger.Error;
                case LogLevel.Critical:
                    return _logger.Fatal;
                case LogLevel.None:
                default:
                    return (s, e) => { };
            }
        }

        private void AppendScopeInformation(StringBuilder messageBuilder)
        {
            var current = Log4NetScope.Current;

            if (current != null)
            {
                messageBuilder.Append($" => {current}");
            }
        }
    }
}
