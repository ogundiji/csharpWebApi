using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiFundamental.Core.Data.Entities;
using WebApiFundamental.Core.Repositories;

namespace WebApiFundamental.Persistence
{
    public class CampRepository:ICampRepository
    {
        private readonly CampContext _context;

        public CampRepository(CampContext context)
        {
            _context = context;
        }

        public void AddCamp(Camp camp)
        {
            _context.camps.Add(camp);
        }  

        public void DeleteCamp(Camp camp)
        {
            _context.camps.Remove(camp);
        }

        public async Task<Camp[]> GetAllCampsByEventDate(DateTime dateTime, bool includeTalks = false)
        {
            IQueryable<Camp> query = _context.camps
                .Include(c => c.Location);

            if (includeTalks)
            {
                query = query
                  .Include(c => c.Talks.Select(t => t.Speaker));
            }

            // Order It
            query = query.OrderByDescending(c => c.EventDate)
              .Where(c => c.EventDate == dateTime);

            return await query.ToArrayAsync();
        }

        public async Task<Camp[]> GetAllCampsAsync(bool includeTalks = false)
        {
            IQueryable<Camp> query = _context.camps
                .Include(c => c.Location);

            if (includeTalks)
            {
                query = query
                  .Include(c => c.Talks.Select(t => t.Speaker));
            }

            // Order It
            query = query.OrderByDescending(c => c.EventDate);

            return await query.ToArrayAsync();
        }

        public async Task<Camp> GetCampAsync(string moniker, bool includeTalks = false)
        {
            IQueryable<Camp> query = _context.camps
                .Include(c => c.Location);

            if (includeTalks)
            {
                query = query.Include(c => c.Talks.Select(t => t.Speaker));
            }

            // Query It
            query = query.Where(c => c.Moniker == moniker);

            return await query.FirstOrDefaultAsync();
        }
    }
}