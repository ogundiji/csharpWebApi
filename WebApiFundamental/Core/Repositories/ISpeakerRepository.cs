using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiFundamental.Core.Data.Entities;

namespace WebApiFundamental.Core.Repositories
{
    public interface ISpeakerRepository
    {
        void AddSpeaker(Speaker speaker);
        void DeleteSpeaker(Speaker speaker);
        Task<Speaker[]> GetSpeakersByMonikerAsync(string moniker);
        Task<Speaker> GetSpeakerAsync(int speakerId);
        Task<Speaker[]> GetAllSpeakersAsync();
    }
}
