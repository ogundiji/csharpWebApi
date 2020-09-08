using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using WebApiFundamental.Core.Data.Entities;
using WebApiFundamental.Core.ViewModel;

namespace WebApiFundamental.Core.Repositories
{
    public interface IAuthRepository
    {
         Task<IdentityResult> RegisterUser(UserModel userModel);
         Task<ApplicationUser> FindUser(string userName, string password);
         Task<bool> ResetPassword(ResetPasswordViewModel model);
         Task<bool> ForgotUser(string email);
         Task<ApplicationUser> ViewUserDetails(string email);
         Task<bool> CheckIfUserExist(string email);
    }
}
