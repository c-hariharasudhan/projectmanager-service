using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Infrastructure.Logging
{
    public enum LogLevel
    {
        Fatal,
        Error,
        Warn,
        Info,
        Debug

    }
    public interface ILogger
    {
        void WriteMessage(Type className, LogLevel level, string message, Exception error = null);
    }
}
