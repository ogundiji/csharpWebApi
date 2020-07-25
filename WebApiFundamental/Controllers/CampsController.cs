using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiFundamental.Core.Data.Entities;
using WebApiFundamental.Core.Models;
using WebApiFundamental.Core.Repositories;

namespace WebApiFundamental.Controllers
{

    [RoutePrefix("api/camps")]
    public class CampsController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private IMapper _mapper;
        public CampsController(IUnitOfWork  unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Route()]
        public async Task<IHttpActionResult> Get(bool includeTalks=false)
        {
            try
            {
                var result = await unitOfWork.camp.GetAllCampsAsync(includeTalks);

                return Ok(_mapper.Map<IEnumerable<CampDto>>(result));
            }
           catch(Exception ex)
            {
                //Add Loggin to catch the exception
                return InternalServerError();
            }
           
        }

        
        [Route("{moniker}", Name ="GetCamp")]
        public async Task<IHttpActionResult> Get(string moniker, bool includeTalks = false)
        {
            try
            {
               var result=await unitOfWork.camp.GetCampAsync(moniker,includeTalks);
                if (result == null)
                    return NotFound();

                return Ok(_mapper.Map<CampDto>(result));

            }catch(Exception ex)
            {
                return InternalServerError();
            }
        }

       

        [Route("SearchEventByDate/{eventDate:datetime}")]
        [HttpGet]
        public async Task<IHttpActionResult>SearchEventByDate(DateTime eventDate,bool includeTalks=false)
        {
            try
            {
                var result = await unitOfWork.camp.GetAllCampsByEventDate(eventDate, includeTalks);
                if (result == null)
                    return NotFound();

                return Ok(_mapper.Map<CampDto[]>(result));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [Route("CreateCamp")]
        [HttpPost]
        public async Task<IHttpActionResult>Post(CampDto model)
        {
            try
            {
                if(await unitOfWork.camp.GetCampAsync(model.Moniker) != null)
                {
                    ModelState.AddModelError("Moniker", "Moniker in Use");
                }
                if (ModelState.IsValid)
                {
                    //do the reverse process of what we did during get
                    var camp = _mapper.Map<Camp>(model);
                    unitOfWork.camp.AddCamp(camp);

                    if (await unitOfWork.SaveChangesAsync())
                    {
                        var newModel = _mapper.Map<CampDto>(camp);
                       
                        return CreatedAtRoute("GetCamp", new { moniker = newModel.Moniker }, newModel);

                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return BadRequest(ModelState);
        }


        [Route("{moniker}")]
        public async Task<IHttpActionResult>Put(string moniker,CampDto model)
        {
            try
            {
                var camp = await unitOfWork.camp.GetCampAsync(moniker);
                if (camp == null) return NotFound();

                _mapper.Map(model,camp);

                if(await unitOfWork.SaveChangesAsync())
                {
                    return Ok(_mapper.Map<CampDto>(camp));
                }
                else
                {
                    return InternalServerError();
                }

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
        
        [Route("{moniker}")]
        public async Task<IHttpActionResult>Delete(string moniker)
        {
            try
            {
                var camp = await unitOfWork.camp.GetCampAsync(moniker);
                if (camp == null) return NotFound();

                unitOfWork.camp.DeleteCamp(camp);

                if(await unitOfWork.SaveChangesAsync())
                {
                    return Ok();
                }
                else
                {
                    return InternalServerError();
                }

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

    }
}
