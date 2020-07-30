using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiFundamental.Core.Repositories
{
    public interface IUnitOfWork
    {
        ICampRepository camp { get; }
        ISpeakerRepository speaker { get; }
        ITalkRepository talk { get; }
        IAuthRepository auth { get; }
        ILogRepository log { get; }
        IEmailService em { get; }
        Task<bool> SaveChangesAsync();
    }
}
