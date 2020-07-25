using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiFundamental.Core.Data.Entities;
using WebApiFundamental.Persistence.Repository;

namespace WebApiFundamental.Core.provider
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (AuthRepository _repo = new AuthRepository())
            {
                ApplicationUser user = await _repo.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);

        }

    }
}
    
   


   
    //public class SimpleAuthorizationServerProvider: OAuthAuthorizationServerProvider
    //{
    //    public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
    //    {
    //      return context.Validated();
    //        //return base.ValidateClientAuthentication(context);
    //    }
    //    //public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
    //    //{
    //    //    context.Validated();
    //    //}

    //}