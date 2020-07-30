using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiFundamental.Core.Data.Entities;
using WebApiFundamental.Core.Repositories;

namespace WebApiFundamental.Persistence.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly CampContext context;
        public LogRepository(CampContext context)
        {
            this.context = context;
        }
        public void Add(ErrorLogger errorLogger)
        {
            context.errorLoggers.Add(errorLogger);
        }
    }
}