using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using WebApiFundamental.Core.Data.Entities;

namespace WebApiFundamental.Core.Repositories
{
    public interface IAuthRepository
    {
         Task<IdentityResult> RegisterUser(UserModel userModel);
         Task<ApplicationUser> FindUser(string userName, string password);
    }
}
