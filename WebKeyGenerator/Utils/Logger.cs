using NLog;
using NLog.Targets;

namespace WebKeyGenerator.Utils
{
    public class Logger
    {
        private readonly NLog.Logger tracelogger;
        private readonly NLog.Logger errorLogger;

        public Logger(string name = "logger")
        {
            tracelogger = NLog.LogManager.GetLogger("traceLogger");
            errorLogger = NLog.LogManager.GetLogger("errorLogger");
        }

        public static Logger Default
        {
            get { return new Logger(); }
        }

        public string CurrentLogFilePath
        {
            get
            {
                var target = (FileTarget)LogManager.Configuration.FindTargetByName("logfile");
                if (target == null)
                    return string.Empty;
                return target.FileName.Render(new LogEventInfo() { LoggerName = tracelogger.Name })
                    .Replace('/', Path.DirectorySeparatorChar)
                    .Replace($"{Path.DirectorySeparatorChar.ToString()}{Path.DirectorySeparatorChar.ToString()}", Path.DirectorySeparatorChar.ToString());
            }
        }

        public virtual void Log(NLog.LogLevel level, string format, params object[] args)
        {
            tracelogger.Log(level, format, args);
        }

        private void Log(NLog.LogLevel level, string source, string context, string message)
        {
            tracelogger.Log(level, $"{source}\t{context}\t{message}");
        }

        public static void Info(string format, params object[] args)
        {
            Default.Log(NLog.LogLevel.Trace, format, args);
        }

        public  void Error(string format, params object[] args)
        {
            errorLogger.Log(NLog.LogLevel.Error, format, args);
        }

        public static void Warning(string format, params object[] args)
        {
            Default.Log(NLog.LogLevel.Warn, format, args);
        }

        public void Trace(string format, params object[] args)
        {
            tracelogger.Log(NLog.LogLevel.Trace, format, args);
        }

        internal static void Write(NLog.LogLevel level, string source, string context, string message)
        {
            Default.Log(level, source, context, message);
        }
    }
}
