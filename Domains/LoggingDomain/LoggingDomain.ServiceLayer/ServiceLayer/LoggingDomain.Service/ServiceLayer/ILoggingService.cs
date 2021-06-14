using LoggingDomain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingDomain.Service.ServiceLayer
{
    public interface ILoggingService
    {
        public void Log(string header, string message, LogSeverity severity);
    }
}
