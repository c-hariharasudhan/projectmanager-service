using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Infrastructure.Logging
{
    public class Logger : ILogger
    {
        public void WriteMessage(Type className, LogLevel level, string message, Exception error = null)
        {
            ILog log = LogManager.GetLogger(className);
            switch (level)
            {
                case LogLevel.Fatal:
                    log.Fatal(message, error);
                    break;
                case LogLevel.Error:
                    log.Error(message, error);
                    break;
                case LogLevel.Warn:
                    log.Warn(message);
                    break;
                case LogLevel.Info:
                    log.Info(message);
                    break;
                case LogLevel.Debug:
                    log.Debug(message);
                    break;
            }
        }
    }
}
