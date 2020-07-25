using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiFundamental.Core.Data.Entities;

namespace WebApiFundamental.Core.Repositories
{
    public interface ITalkRepository
    {
        void AddTalk(Talk talk);
        void DeleteTalk(Talk talk);
        Task<Talk> GetTalkByMonikerAsync(string moniker, int talkId, bool includeSpeakers = false);
        Task<Talk[]> GetTalksByMonikerAsync(string moniker, bool includeSpeakers = false);
    }
}
