using LoggingDomain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using Utilities;

namespace LoggingDomain.Infrastructure.InfrastructureLayer
{
    public class LoggingDbContext:IDbContext
    {
        public DbSet<LogMessage> LogMessages { get; set; }
    }
}
