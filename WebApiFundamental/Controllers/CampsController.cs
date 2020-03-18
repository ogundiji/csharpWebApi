﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiFundamental.Data;
using WebApiFundamental.Data.Entities;
using WebApiFundamental.Models;

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
        public async Task<IHttpActionResult> Get(bool includeTalks=false)
        {
            try
            {
                var result = await _repository.GetAllCampsAsync(includeTalks);

                return Ok(_mapper.Map<IEnumerable<CampModel>>(result));
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
               var result=await _repository.GetCampAsync(moniker,includeTalks);
                if (result == null)
                    return NotFound();

                return Ok(_mapper.Map<CampModel>(result));

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
                var result = await _repository.GetAllCampsByEventDate(eventDate, includeTalks);
                if (result == null)
                    return NotFound();

                return Ok(_mapper.Map<CampModel[]>(result));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [Route("CreateCamp")]
        [HttpPost]
        public async Task<IHttpActionResult>Post(CampModel model)
        {
            try
            {
                if(await _repository.GetCampAsync(model.Moniker) != null)
                {
                    ModelState.AddModelError("Moniker", "Moniker in Use");
                }
                if (ModelState.IsValid)
                {
                    //do the reverse process of what we did during get
                    var camp = _mapper.Map<Camp>(model);
                    _repository.AddCamp(camp);

                    if (await _repository.SaveChangesAsync())
                    {
                        var newModel = _mapper.Map<CampModel>(camp);
                       
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
        public async Task<IHttpActionResult>Put(string moniker,CampModel model)
        {
            try
            {
                var camp = await _repository.GetCampAsync(moniker);
                if (camp == null) return NotFound();

                _mapper.Map(model,camp);

                if(await _repository.SaveChangesAsync())
                {
                    return Ok(_mapper.Map<CampModel>(camp));
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
                var camp = await _repository.GetCampAsync(moniker);
                if (camp == null) return NotFound();

                _repository.DeleteCamp(camp);

                if(await _repository.SaveChangesAsync())
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
