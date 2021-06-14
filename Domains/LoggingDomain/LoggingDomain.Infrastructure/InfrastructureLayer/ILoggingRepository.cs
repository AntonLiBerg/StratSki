using LoggingDomain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace LoggingDomain.Infrastructure.InfrastructureLayer
{
    public interface ILoggingRepository : IRepository<LogMessage>
    {
    }
}
