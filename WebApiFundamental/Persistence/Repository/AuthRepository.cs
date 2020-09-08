using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiFundamental.Core.Data.Entities;
using WebApiFundamental.Core.Repositories;
using WebApiFundamental.Core.ViewModel;

namespace WebApiFundamental.Persistence.Repository
{
        public class AuthRepository :IAuthRepository,IDisposable
        {
            private CampContext _ctx;
            private readonly IEmailService sc;

            private UserManager<ApplicationUser> _userManager;

            public AuthRepository()
            {
                 sc = new EmailServiceRepository();
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
                    Email=userModel.Email,
                    UserName = userModel.Email
                };

                var result = await _userManager.CreateAsync(user, userModel.Password);
             

                return result;
            }
            
        public async Task<bool> ForgotUser(string email)
        {
            bool check = true;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                check = false;
            }
            string code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
            sc.SendEmail(user.Email, user.LastName, "Reset Password", $"Please reset your password by using this {code} ");

            return check;
        }

       
        public async Task<bool> ResetPassword(ResetPasswordViewModel model)
        {
            bool check = false;
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                check = false;
            }

            var result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);

            if (result.Succeeded)
            {
                check = true;
            }

            return check;
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

        public async Task<ApplicationUser> ViewUserDetails(string email)
        {
            return await _ctx.Users.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<bool> CheckIfUserExist(string email)
        {
            return await _ctx.Users.AnyAsync(x => x.Email == email);
        }
    }
    }