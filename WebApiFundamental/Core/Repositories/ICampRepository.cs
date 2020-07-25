using System;
using System.Threading.Tasks;
using WebApiFundamental.Core.Data.Entities;

namespace WebApiFundamental.Core.Repositories
{
    public interface ICampRepository
    {
        // General 
       

        // Camps
        void AddCamp(Camp camp);
        void DeleteCamp(Camp camp);
        Task<Camp[]> GetAllCampsAsync(bool includeTalks = false);
        Task<Camp> GetCampAsync(string moniker, bool includeTalks = false);
        Task<Camp[]> GetAllCampsByEventDate(DateTime dateTime, bool includeTalks = false);

        // Talks
       

        // Speakers
       
    }
}
