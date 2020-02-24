using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiFundamental.Data;

namespace WebApiFundamental.Controllers
{
    public class CampsController : ApiController
    {
        private readonly ICampRepository _repository;
        public CampsController(ICampRepository repository)
        {
            _repository = repository;
        }
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAllCampsAsync();
                return Ok(result);
            }
            catch(Exception ex)
            {
                //Add Loggin to catch the exception
                return InternalServerError();
            }
           
        }

    }
}
