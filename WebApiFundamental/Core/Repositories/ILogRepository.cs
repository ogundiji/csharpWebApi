using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiFundamental.Core.Data.Entities;

namespace WebApiFundamental.Core.Repositories
{
   public interface ILogRepository
   {
        void Add(ErrorLogger errorLogger);
   }
}
