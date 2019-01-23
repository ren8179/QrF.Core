using Microsoft.Extensions.Logging;

namespace QrF.Core.Log4Net
{
    public static class Log4NetExtensions
    {
        /// <summary>
        /// Adds the log4 net.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="log4NetConfigFile">The log4 net configuration file.</param>
        /// <param name="watch">if set to <c>true</c> [watch].</param>
        /// <returns>The <see cref="ILoggerFactory"/> with added Log4Net provider</returns>
        public static ILoggerFactory AddLog4Net(
            this ILoggerFactory factory,
            string log4NetConfigFile,
            bool watch)
        {
            factory.AddProvider(new Log4NetProvider(log4NetConfigFile, watch));
            return factory;
        }
        public static ILoggerFactory AddLog4Net(
            this ILoggerFactory factory,
            string log4NetConfigFile,
            bool watch,
            string repository)
        {
            factory.AddProvider(new Log4NetProvider(log4NetConfigFile, watch, repository));
            return factory;
        }
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory)
        {
            factory.AddProvider(new Log4NetProvider());
            return factory;
        }
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, bool watch, string repository)
        {
            factory.AddProvider(new Log4NetProvider(watch, repository));
            return factory;
        }
        public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder)
        {
            builder.SetMinimumLevel(LogLevel.Trace);
            builder.AddProvider(new Log4NetProvider());

            return builder;
        }

        public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder, string log4NetConfigFile, bool watch, string repository)
        {
            builder.SetMinimumLevel(LogLevel.Trace);
            builder.AddProvider(new Log4NetProvider(log4NetConfigFile, watch, repository));

            return builder;
        }
        public static ILoggingBuilder AddLog4Net(this ILoggingBuilder factory, bool watch, string repository)
        {
            factory.AddProvider(new Log4NetProvider(watch, repository));
            return factory;
        }
    }
}
