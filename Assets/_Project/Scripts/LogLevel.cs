using System;

namespace com.ColorSelect
{
    [Flags]
    public enum LogLevel
    {
        Log = 0,
        Warning = 1 << 0,
        Error = 1 << 2,
    }
}