using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiFundamental.Core.Data.Entities;
using WebApiFundamental.Core.Models;
using WebApiFundamental.Core.Repositories;

namespace WebApiFundamental.Controllers
{
    [RoutePrefix("api/Speakers")]
    public class SpeakersController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public SpeakersController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [Route()]
        public async Task<IHttpActionResult> Get()
        {
            var result = await unitOfWork.speaker.GetAllSpeakersAsync();
            return Ok(mapper.Map<IEnumerable<SpeakerDto>>(result));
        }

        [Route("Id:int")]
        public async Task<IHttpActionResult>Get(int Id)
        {
            var result = await unitOfWork.speaker.GetSpeakerAsync(Id);
            return Ok(mapper.Map<SpeakerDto>(result));
        }


        [Route("CreateSpeaker")]
        public async Task<IHttpActionResult> Post(SpeakerDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await unitOfWork.speaker.GetSpeakerAsync(model.SpeakerId);

                    if (result == null)
                    {
                        Speaker sp = mapper.Map<Speaker>(model);
                        unitOfWork.speaker.AddSpeaker(sp);

                        if (await unitOfWork.SaveChangesAsync())
                        {
                            return CreatedAtRoute("GetSpeaker", new { id = sp.SpeakerId }, mapper.Map<SpeakerDto>(sp));
                        }

                    }
                    else
                    {
                        return BadRequest("Speaker exist");
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return BadRequest(ModelState);
        }

        [Route("{SpeakerId:int}")]
        public async Task<IHttpActionResult> Put(int SpeakerId,SpeakerDto speakerDto)
        {
            try
            {
                var speaker = await unitOfWork.speaker.GetSpeakerAsync(SpeakerId);
                if (speaker == null) return NotFound();

                mapper.Map(speakerDto, speaker);

                if(await unitOfWork.SaveChangesAsync())
                {
                    return Ok(mapper.Map<SpeakerDto>(speaker));
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return BadRequest();
        }

        [Route("{speakerId:int}")]
        public async Task<IHttpActionResult>Delete(int speakerId)
        {
            try
            {
                var speaker = await unitOfWork.speaker.GetSpeakerAsync(speakerId);
                if (speaker == null) return NotFound();

                unitOfWork.speaker.DeleteSpeaker(speaker);

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
