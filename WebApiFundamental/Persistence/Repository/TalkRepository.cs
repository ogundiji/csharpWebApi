using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiFundamental.Core.Data.Entities;
using WebApiFundamental.Core.Repositories;

namespace WebApiFundamental.Persistence.Repository
{
    public class TalkRepository:ITalkRepository
    {
        private readonly CampContext _context;
        public TalkRepository(CampContext context)
        {
            this._context = context;
        }
        public void AddTalk(Talk talk)
        {
            _context.talks.Add(talk);
        }

        public void DeleteTalk(Talk talk)
        {
            _context.talks.Remove(talk);
        }
        public async Task<Talk[]> GetTalksByMonikerAsync(string moniker, bool includeSpeakers = false)
        {
            IQueryable<Talk> query = _context.talks;

            if (includeSpeakers)
            {
                query = query
                  .Include(t => t.Speaker);
            }

            // Add Query
            query = query
              .Where(t => t.Camp.Moniker == moniker)
              .OrderByDescending(t => t.Title);

            return await query.ToArrayAsync();
        }

        public async Task<Talk> GetTalkByMonikerAsync(string moniker, int talkId, bool includeSpeakers = false)
        {
            IQueryable<Talk> query = _context.talks;

            if (includeSpeakers)
            {
                query = query
                  .Include(t => t.Speaker);
            }

            // Add Query
            query = query
              .Where(t => t.TalkId == talkId && t.Camp.Moniker == moniker);

            return await query.FirstOrDefaultAsync();
        }
    }
}