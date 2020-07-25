using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebApiFundamental.Core.Data.Entities;
using WebApiFundamental.Core.Repositories;

namespace WebApiFundamental.Persistence.Repository
{
    public class SpeakerRepository:ISpeakerRepository
    {
        private readonly CampContext _context;
        public SpeakerRepository(CampContext context)
        {
            this._context = context;
        }
        public void AddSpeaker(Speaker speaker)
        {
            _context.speakers.Add(speaker);
        }

        public void DeleteSpeaker(Speaker speaker)
        {
            _context.speakers.Remove(speaker);
        }

        public async Task<Speaker[]> GetSpeakersByMonikerAsync(string moniker)
        {
            IQueryable<Speaker> query = _context.talks
              .Where(t => t.Camp.Moniker == moniker)
              .Select(t => t.Speaker)
              .Where(s => s != null)
              .OrderBy(s => s.LastName)
              .Distinct();

            return await query.ToArrayAsync();
        }
        public async Task<Speaker[]> GetAllSpeakersAsync()
        {
            var query = _context.speakers
              .OrderBy(t => t.LastName);

            return await query.ToArrayAsync();
        }


        public async Task<Speaker> GetSpeakerAsync(int speakerId)
        {
            var query = _context.speakers
              .Where(t => t.SpeakerId == speakerId);

            return await query.FirstOrDefaultAsync();
        }
    }
}