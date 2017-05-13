using System;
using NLog;

namespace Texas_Hold_em.Loggers
{
    public class ActiveLogger : IGameLogger
    {
        private readonly Logger _logger;

        public ActiveLogger(Logger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("No logger was received in " + GetType().Name);
            }
            _logger = logger;
        }
        

        public void Debug(string msg)
        {
            _logger.Debug(msg);
        }

        public void Error(string msg)
        {
            _logger.Error(msg);
        }

        public void Warn(string msg)
        {
            _logger.Warn(msg);
        }
        
    }
}
