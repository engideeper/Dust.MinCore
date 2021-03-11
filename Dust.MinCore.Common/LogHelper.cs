using Serilog;
using System;

namespace Dust.MinCore.Common
{
    /// <summary>
    /// 日志管理
    /// </summary>
    public class LogHelper
    {
        private readonly ILogger _logger;

        private LogHelper(ILogger logger)
        {
            _logger = logger;
        }

        public static LogHelper Default { get; private set; }

        static LogHelper()
        {
            Default = new LogHelper(Log.Logger);
        }

        #region Trace

        public void Trace(string msg, params object[] args)
        {
            _logger.Verbose(msg, args);
        }

        public void Trace(string msg, Exception err)
        {
            _logger.Verbose(err, msg);
        }

        #endregion Trace

        #region Debug

        public void Debug(string msg, params object[] args)
        {
            _logger.Debug(msg, args);
        }

        public void Debug(string msg, Exception err)
        {
            _logger.Debug(err, msg);
        }

        #endregion Debug

        #region Info

        public void Info(string msg, params object[] args)
        {
            _logger.Information(msg, args);
        }

        public void Info(string msg, Exception err)
        {
            _logger.Information(err, msg);
        }

        #endregion Info

        #region Warn

        public void Warn(string msg, params object[] args)
        {
            _logger.Warning(msg, args);
        }

        public void Warn(string msg, Exception err)
        {
            _logger.Warning(err, msg);
        }

        #endregion Warn

        #region Error

        public void Error(string msg, params object[] args)
        {
            _logger.Error(msg, args);
        }

        public void Error(string msg, Exception err)
        {
            _logger.Error(err, msg);
        }

        #endregion Error

        #region Fatal

        public void Fatal(string msg, params object[] args)
        {
            _logger.Fatal(msg, args);
        }

        public void Fatal(string msg, Exception err)
        {
            _logger.Fatal(err, msg);
        }

        #endregion Fatal
    }
}
