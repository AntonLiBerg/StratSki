using LoggingDomain.Infrastructure.InfrastructureLayer;
using LoggingDomain.Model.Entities;
using LoggingDomain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace LoggingDomain.Service.ServiceLayer
{
    public class LoggingService : ILoggingService
    {
        protected ILoggingRepository _loggingRepository;
        public LoggingService()
        {
            _loggingRepository = new LoggingRepository();
        }
        public LoggingService(ILoggingRepository repository)
        {
            _loggingRepository = repository;
        }
        public void Log(string header, string message, LogSeverity severity)
        {
            _loggingRepository.Create(new LogMessage(header,message,DateTime.Now,severity));
        }
    }
}
