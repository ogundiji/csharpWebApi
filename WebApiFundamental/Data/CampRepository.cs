using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiFundamental.Data.Entities;

namespace WebApiFundamental.Data
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

        public void AddTalk(Talk talk)
        {
            _context.talks.Add(talk);
        }

        public void AddSpeaker(Speaker speaker)
        {
            _context.speakers.Add(speaker);
        }

        public void DeleteCamp(Camp camp)
        {
            _context.camps.Remove(camp);
        }

        public void DeleteTalk(Talk talk)
        {
            _context.talks.Remove(talk);
        }

        public void DeleteSpeaker(Speaker speaker)
        {
            _context.speakers.Remove(speaker);
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
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