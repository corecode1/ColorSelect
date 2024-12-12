using UnityEngine;

namespace com.ColorSelect
{
    public class Logger : ILogger
    {
        private LogLevel _logLevel;

        public Logger(Config config)
        {
            _logLevel = config.LogLevel;
        }

        public void Log(string message, LogLevel level)
        {
            if (!_logLevel.HasFlag(level))
            {
                return;
            }

            LogConstructedMessage(message, level);
        }

        public void Log(string message, LogLevel level, object[] args)
        {
            if (!_logLevel.HasFlag(level))
            {
                return;
            }

            LogConstructedMessage(string.Format(message, args), level);
        }

        public void SetLogLevel(LogLevel level)
        {
            _logLevel = level;
        }

        private void LogConstructedMessage(string message, LogLevel level)
        {
            LogToUnity(message, level);
            // log to screen, db, devconsole, etc
        }

        private void LogToUnity(string message, LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Error:
                    Debug.LogError(message);
                    break;
                case LogLevel.Warning:
                    Debug.LogWarning(message);
                    break;
                default:
                case LogLevel.Log:
                    Debug.Log(message);
                    break;
            }
        }
    }
}