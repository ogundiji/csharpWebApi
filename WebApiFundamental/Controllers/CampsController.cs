using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiFundamental.Data;
using WebApiFundamental.ViewModel;

namespace WebApiFundamental.Controllers
{
    [RoutePrefix("api/camps")]
    public class CampsController : ApiController
    {
        private readonly ICampRepository _repository;
        private IMapper _mapper;
        public CampsController(ICampRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [Route()]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAllCampsAsync();
                return Ok(_mapper.Map<IEnumerable<CampViewModel>>(result));
            }
            catch(Exception ex)
            {
                //Add Loggin to catch the exception
                return InternalServerError();
            }
           
        }

        [Route("{moniker}")]
        public async Task<IHttpActionResult> Get(string moniker)
        {
            try
            {
               var result=await _repository.GetCampAsync(moniker);
                if (result == null)
                    return NotFound();

                return Ok(_mapper.Map<CampViewModel>(result));

            }catch(Exception ex)
            {
                return InternalServerError();
            }
        }

    }
}
