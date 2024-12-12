namespace com.ColorSelect
{
    public static class ILoggerExtensions
    {
        private const int MaxNonAllocParams = 4;
        private static readonly object[] _paramsCache = new object[MaxNonAllocParams];

        public static void LogRegular(this ILogger logger, string message)
        {
            logger.Log(message, LogLevel.Log);
        }

        public static void LogWarning(this ILogger logger, string message)
        {
            logger.Log(message, LogLevel.Warning);
        }

        public static void LogError(this ILogger logger, string message)
        {
            logger.Log(message, LogLevel.Error);
        }
        
        public static void Log(this ILogger logger, string message, LogLevel level, object param1)
        {
            _paramsCache[0] = param1;
            
            logger.Log(message, level, _paramsCache);
        }

        public static void Log(this ILogger logger, string message, LogLevel level, object param1, object param2)
        {
            _paramsCache[0] = param1;
            _paramsCache[1] = param2;

            logger.Log(message, level, _paramsCache);
        }

        public static void Log(
            this ILogger logger,
            string message,
            LogLevel level,
            object param1,
            object param2,
            object param3)
        {
            _paramsCache[0] = param1;
            _paramsCache[1] = param2;
            _paramsCache[2] = param3;

            logger.Log(message, level, _paramsCache);
        }

        public static void Log(
            this ILogger logger,
            string message,
            LogLevel level,
            object param1,
            object param2,
            object param3,
            object param4)
        {
            _paramsCache[0] = param1;
            _paramsCache[1] = param2;
            _paramsCache[2] = param3;
            _paramsCache[3] = param4;

            logger.Log(message, level, _paramsCache);
        }

        public static void Log(this ILogger logger, string message, LogLevel level, params object[] args)
        {
            logger.Log(string.Format(message, args), level);
        }
    }
}