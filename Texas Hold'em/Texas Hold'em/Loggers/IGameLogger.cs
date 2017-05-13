using NLog;

namespace Texas_Hold_em.Loggers
{
    public interface IGameLogger
    {
        void Debug(string msg);
        void Error(string msg);
        void Warn(string msg);
    }
}
