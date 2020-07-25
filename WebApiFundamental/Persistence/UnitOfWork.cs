using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiFundamental.Core.Repositories;
using WebApiFundamental.Persistence.Repository;

namespace WebApiFundamental.Persistence
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly CampContext _context;
        public ITalkRepository talk { get; private set; }
        public ISpeakerRepository speaker { get; private set; }
        public ICampRepository camp { get; private set; }
        public IAuthRepository auth { get; private set; }
        public UnitOfWork(CampContext context)
        {
            _context = context;
            talk = new TalkRepository(_context);
            speaker = new SpeakerRepository(_context);
            camp = new CampRepository(_context);
            auth = new AuthRepository(_context);
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}