using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiFundamental.Core.Data.Entities;
using WebApiFundamental.Core.Repositories;

namespace WebApiFundamental.Persistence.Repository
{
        public class AuthRepository :IAuthRepository,IDisposable
        {
            private CampContext _ctx;


            private UserManager<ApplicationUser> _userManager;

            public AuthRepository()
            {
                _ctx = new CampContext();
                _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
            }
            public AuthRepository(CampContext context)
            {
                 _ctx = context;
            }

            public async Task<IdentityResult> RegisterUser(UserModel userModel)
            {
                ApplicationUser user = new ApplicationUser
                {
                    FirstName=userModel.FirstName,
                    LastName=userModel.LastName,
                    Email=userModel.UserName,
                    UserName = userModel.UserName
                };

                var result = await _userManager.CreateAsync(user, userModel.Password);
             

                return result;
            }

            public async Task<ApplicationUser> FindUser(string userName, string password)
            {
                ApplicationUser user = await _userManager.FindAsync(userName, password);

                return user;
            }
     

            public void Dispose()
            {
                _ctx.Dispose();
                _userManager.Dispose();

            }
        }
    }