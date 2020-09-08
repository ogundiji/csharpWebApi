using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiFundamental.Core.Data.Entities;
using WebApiFundamental.Core.Model;
using WebApiFundamental.Core.Repositories;
using WebApiFundamental.Core.ViewModel;

namespace WebApiFundamental.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        IMapper mapper;
        private IUnitOfWork unitOfWork;

        public AccountController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register([FromBody]UserModel userModel)
        {
            if (!ModelState.IsValid )
            {
                return BadRequest(ModelState);
            }

            if (await unitOfWork.auth.CheckIfUserExist(userModel.Email))
            {
                return BadRequest("user account exist");
            }

            IdentityResult result = await unitOfWork.auth.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }


        [Route("ForgotPassword")]
        [HttpPost]
        public async Task<IHttpActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            bool check = false;
            if (ModelState.IsValid)
            {
                check = await unitOfWork.auth.ForgotUser(model.Email);

                if (check)
                {
                    return Ok("Successfull");
                }
                else
                {
                    return BadRequest("Not successfull");
                }
            }
            return BadRequest(ModelState);
        }

        
        //api/Account/{email}
       
     
        [HttpGet]
        [Authorize]
        [Route("{email}")]
        public async Task<IHttpActionResult> Get(string email)
        {     
            try
            {
                email += "@gmail.com";
                var polly = await unitOfWork.auth.ViewUserDetails(email);
                if (polly == null)
                    return NotFound();
                return Ok(mapper.Map<ApplicationDTO>(polly));
            }
            catch
            {
                return InternalServerError();
            }
        }

        [Route("ResetPassword")]
        [HttpPost]
        public async Task<IHttpActionResult> ResetUserPassword(ResetPasswordViewModel model)
        {
            bool check = false;
            if (ModelState.IsValid)
            {
                check =await unitOfWork.auth.ResetPassword(model);

                if (check)
                {
                    return Ok("Password successfully reset");
                }
                else
                {
                    return BadRequest("password reset not successfull");
                }
            }

            return BadRequest(ModelState);
        }


        private IHttpActionResult GetErrorResult(IdentityResult result)
            {
                if (result == null)
                {
                    return InternalServerError();
                }

                if (!result.Succeeded)
                {
                    if (result.Errors != null)
                    {
                        foreach (string error in result.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }

                    if (ModelState.IsValid)
                    {
                        // No ModelState errors are available to send, so just return an empty BadRequest.
                        return BadRequest();
                    }

                    return BadRequest(ModelState);
                }

                return null;
            }

        }
    }
