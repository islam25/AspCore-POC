
using System;

namespace School.Core.ILoggerService
{
    public interface ILoggerManager : IDisposable
    {
        ILoggerManager CreateLogger();
        void LogDebug(string message);
        void LogError(string message, Exception exception = null);
        void LogInformation(string message);
        void LogTrace(string message);
        void LogWarning(string message);
    }
}
