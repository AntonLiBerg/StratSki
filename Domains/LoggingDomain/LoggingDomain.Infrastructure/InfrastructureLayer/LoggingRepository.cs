using LoggingDomain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoggingDomain.Infrastructure.InfrastructureLayer
{
    public class LoggingRepository : ILoggingRepository
    {
        private LoggingDbContext _dbContext;
        public LoggingRepository(LoggingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public LoggingRepository()
        {
            _dbContext = new LoggingDbContext();
        }
        public void Create(LogMessage entity)
        {
            _dbContext.LogMessages.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LogMessage> Get()
        {
            throw new NotImplementedException();
        }

        public LogMessage Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(LogMessage entity)
        {
            throw new NotImplementedException();
        }
    }
}
