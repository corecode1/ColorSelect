namespace com.ColorSelect
{
    public interface ILogger
    {
        public void Log(string message, LogLevel level);
        public void Log(string message, LogLevel level, object[] args);
        public void SetLogLevel(LogLevel level);
    }
}