using LoggingDomain.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LoggingDomain.Model.Entities
{
    public class LogMessage
    {
        public Guid Id { get; set; }
        public string Header { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }
        public LogSeverity Severity { get; set; }
        public LogMessage(string header, string details,DateTime timestamp, LogSeverity severity)
        {
            Id = Guid.NewGuid();
            Header = header;
            Details = details;
            Timestamp = timestamp;
            Severity = severity;
        }

    }
}
