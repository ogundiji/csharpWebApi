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
    [RoutePrefix("api/camps/{moniker}/talks")]
    public class TalksController : ApiController
    {
        private readonly IUnitOfWork  unitOfWork;
        private readonly IMapper _mapper;

        public TalksController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Route()]
        public async Task<IHttpActionResult> Get(string moniker,bool includeSpeakers=false)
        {
            try
            {
                var result = await unitOfWork.talk.GetTalksByMonikerAsync(moniker,includeSpeakers);

                return Ok(_mapper.Map<IEnumerable<TalkDto>>(result));
   
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [Route("{id:int}",Name ="GetTalk")]
        public async Task<IHttpActionResult>Get(string moniker,int id, bool includeSpeakers = false)
        {
            try
            {
                var result = await unitOfWork.talk.GetTalkByMonikerAsync(moniker, id,includeSpeakers);
                if (result == null) return NotFound();

                return Ok(_mapper.Map<TalkDto>(result));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [Route()]
        public async Task<IHttpActionResult>Post(string moniker,TalkDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var camp = await unitOfWork.camp.GetCampAsync(moniker);
                    if (camp != null)
                    {
                        var talk = _mapper.Map<Talk>(model);
                        talk.Camp = camp;

                        //map the speaker if necessary
                        if (model.Speaker != null)
                        {
                            var speaker =await  unitOfWork.speaker.GetSpeakerAsync(model.Speaker.SpeakerId);
                            if (speaker != null)
                            {
                                talk.Speaker = speaker;
                            }
                        }
                        unitOfWork.talk.AddTalk(talk);


                        if(await unitOfWork.SaveChangesAsync())
                        {
                            return CreatedAtRoute("GetTalk", 
                                new { moniker = moniker, id = talk.TalkId }
                            ,_mapper.Map<TalkDto>(talk));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
            return BadRequest(ModelState);
        }

        [Route("{talkId:int}")]
        public async Task<IHttpActionResult>Put(string moniker,int talkId,TalkDto model)
        {
            try
            {
                var talk =await unitOfWork.talk.GetTalkByMonikerAsync(moniker, talkId, true);
                if (talk == null) return NotFound();

                

                if (talk.Speaker.SpeakerId != model.Speaker.SpeakerId)
                {
                    var speaker = await unitOfWork.speaker.GetSpeakerAsync(model.Speaker.SpeakerId);
                    if (speaker != null) talk.Speaker = speaker;
                }

                _mapper.Map(model, talk);

                if (await unitOfWork.SaveChangesAsync())
                {
                    return Ok(_mapper.Map<TalkDto>(talk));
                }


            }
            catch (Exception ex)
            {
                return InternalServerError();
            }

           return BadRequest();
        }

        [Route("{talkId:int}")]
        public async Task<IHttpActionResult>Delete(string moniker,int talkId)
        {
            try
            {
                var talk = await unitOfWork.talk.GetTalkByMonikerAsync(moniker, talkId);
                if (talk == null) return NotFound();

                unitOfWork.talk.DeleteTalk(talk);

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
