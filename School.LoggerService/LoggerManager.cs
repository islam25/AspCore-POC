using NLog;
using School.Core.ILoggerService;
using System;
using System.Diagnostics;

namespace School.LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        #region [Fields]

        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static string CallerMethodMember { get; set; }
        private static string CallerClassMember { get; set; }

        #endregion

        #region [Methods]

        public ILoggerManager CreateLogger()
        {
            var frame = new StackFrame(3);

            CallerMethodMember = frame.GetMethod().Name;

            CallerClassMember = frame.GetMethod().DeclaringType.FullName;

            _logger.Info($"{CallerClassMember} | {CallerMethodMember} Method is Invoked");

            return new LoggerManager();
        }

        public void Dispose()
        {
            _logger.Info($"{CallerClassMember} | {CallerMethodMember} Method is Ended");
        }

        #endregion

        #region [Log Trace]

        public void LogTrace(string message)
        {
            if (!_logger.IsTraceEnabled) return;

            _logger.Trace($"{CallerClassMember} | {CallerMethodMember} | {message}");
        }

        #endregion

        #region [Log Debug]

        public void LogDebug(string message)
        {
            if (!_logger.IsDebugEnabled) return;

            _logger.Debug($"{CallerClassMember} | {CallerMethodMember} | {message}");
        }

        #endregion

        #region [Log Information]

        public void LogInformation(string message)
        {
            if (!_logger.IsInfoEnabled) return;

            _logger.Info($"{CallerClassMember} | {CallerMethodMember} | {message}");
        }

        #endregion

        #region [Log Warning]

        public void LogWarning(string message)
        {
            if (!_logger.IsWarnEnabled) return;

            _logger.Warn($"{CallerClassMember} | {CallerMethodMember} | {message}");
        }

        #endregion

        #region [Log Error]

        public void LogError(string message, Exception exception = null)
        {
            if (!_logger.IsErrorEnabled) return;

            if (exception != null)
            {
                _logger.Error($"{CallerClassMember} | {CallerMethodMember} | {message} | {exception}");
            }
            else
            {
                _logger.Error($"{CallerClassMember} | {CallerMethodMember} | {message}");
            }
        }

        #endregion
    }
}
