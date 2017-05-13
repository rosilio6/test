using System;
using NLog;

namespace Texas_Hold_em.Loggers
{
    public static class LoggerFactory
    {
        private static readonly string ActiveLoggerName= "ActiveLogger";
        private static readonly string ErrorLoggerName= "ErrorLogger";

        private static IGameLogger _activeLogger;
        private static IGameLogger _errorLogger;

        public static IGameLogger GetActiveLogger()
        {
            return _activeLogger ?? (_activeLogger = new ActiveLogger(LogManager.GetLogger(ActiveLoggerName)));
        }

        public static IGameLogger GetErrorLogger()
        {
            return _errorLogger ?? (_errorLogger = new ErrorLogger(LogManager.GetLogger(ErrorLoggerName)));
        }
    }
}
