﻿using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiFundamental.Core.Data.Entities;
using WebApiFundamental.Core.Repositories;

namespace WebApiFundamental.Controllers
{
        [RoutePrefix("api/Account")]
        public class AccountController : ApiController
        {
            private IUnitOfWork unitOfWork;

            public AccountController(IUnitOfWork unitOfWork)
            {
               this.unitOfWork = unitOfWork;
            }

            // POST api/Account/Register
            [AllowAnonymous]
            [Route("Register")]
            public async Task<IHttpActionResult> Register(UserModel userModel)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                IdentityResult result = await unitOfWork.auth.RegisterUser(userModel);

                IHttpActionResult errorResult = GetErrorResult(result);

                if (errorResult != null)
                {
                    return errorResult;
                }

                return Ok();
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
