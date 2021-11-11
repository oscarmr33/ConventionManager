using Conventions.API.Extensions;
using Conventions.API.Repositories;
using Conventions.API.Repositories.Interfaces;
using Conventions.Models.Dto;
using Conventions.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Conventions.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TalksController : ControllerBase
    {
        private readonly ITalksRepository _talkRepository;
        private readonly IConventionRepository _conventionRepository;
        private readonly IUserRepository _userRepository;

        public TalksController(ITalksRepository talksRepository, IConventionRepository conventionRepository, IUserRepository peopleRepository)
        {
            _talkRepository = talksRepository;
            _conventionRepository = conventionRepository;
            _userRepository = peopleRepository;
        }

        [HttpGet]
        public IEnumerable<TalkDto> GetTalks()
        {
            return _talkRepository.GetTalks().Select(talk => talk.AsDto(_userRepository, _conventionRepository));
        }

        [HttpGet("{id}")]
        public ActionResult<TalkDto> GetTalk(Guid id)
        {
            try
            {
                var talk = _talkRepository.GetTalk(id);
                if (talk is null)
                {
                    return NotFound();
                }

                return talk.AsDto(_userRepository, _conventionRepository);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occured while Getting the talk: {e.Message}");
            }
        }

        //POST /talks
        [HttpPost]
        public ActionResult<TalkDto> CreateTalk(CreateTalkDto createTalk)
        {
            try
            {
                if (_conventionRepository.GetConvention(createTalk.ConventionId) == null)
                {
                    return StatusCode(404, "Convention not found");
                }

                if (_userRepository.GetUser(createTalk.SpeakerId) == null)
                {
                    return StatusCode(404, "Speaker not found");
                }

                Talk talk = new()
                {
                    Id = Guid.NewGuid(),
                    Name = createTalk.Name,
                    StartDate = createTalk.StartDate,
                    LengthHours = createTalk.LengthHours,
                    Description = createTalk.Description,
                    AttendeesId = createTalk.AttendeesId,
                    LocationId = createTalk.LocationId,
                    ConventionId = createTalk.ConventionId,
                    SpeakerId = createTalk.SpeakerId
                };

                _talkRepository.CreateTalk(talk);

                return CreatedAtAction(nameof(GetTalk), new { id = talk.Id }, talk.AsDto(_userRepository, _conventionRepository));
            }
            catch(Exception e)
			{
                return StatusCode(500, $"An error has occured while Creating a talk: {e.Message}");
			}
        }

        //PUT /talks/id
        [HttpPut("{id}")]
        public ActionResult UpdateTalk(Guid id, UpdateTalkDto updateTalkDto)
        {
            try
            {
                var existing = _talkRepository.GetTalk(id);
                if (existing == null)
                {
                    return NotFound();
                }

                if (_conventionRepository.GetConvention(updateTalkDto.ConventionId) == null)
                {
                    return StatusCode(404, "Convention not found");
                }

                if (_userRepository.GetUser(updateTalkDto.SpeakerId) == null)
                {
                    return StatusCode(404, "Speaker not found");
                }

                existing.Name = updateTalkDto.Name;
                existing.StartDate = updateTalkDto.StartDate;
                existing.LengthHours = updateTalkDto.LengthHours;
                existing.Description = updateTalkDto.Description;
                existing.AttendeesId = updateTalkDto.AttendeesId;
                existing.LocationId = updateTalkDto.LocationId;
                existing.ConventionId = updateTalkDto.ConventionId;
                existing.SpeakerId = updateTalkDto.SpeakerId;

                _talkRepository.UpdateTalk(existing);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occured while Updating the talk: {e.Message}");
            }
        }

        //DELETE /talks/id
        [HttpDelete("{id}")]
        public ActionResult DeleteTalk(Guid id)
        {
            try
            {
                var existing = _talkRepository.GetTalk(id);
                if (existing == null)
                {
                    return NotFound();
                }

                _talkRepository.DeleteTalk(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occured while Deleting the talk: {e.Message}");
            }
        }

        //GET /talks/id?attendeeId
        [HttpGet("register/{id}")]
        public ActionResult RegisterToTalk(Guid id, Guid attendeeId)
		{
            try
            {
                var existing = _talkRepository.GetTalk(id);
                if (existing == null)
                {
                    return NotFound();
                }
                if (_userRepository.GetUser(attendeeId) == null)
                {
                    return StatusCode(404, "Attendee not found");
                }
                if (!existing.AttendeesId.Contains(attendeeId))
                {
                    existing.AttendeesId.Add(attendeeId);
                }

                return NoContent();
            }
            catch(Exception e)
			{
                return StatusCode(500, $"An error has occurred while Registering user to the talk: {e.Message}");
			}
        }

        //GET /talks/id&attendeeId
        [HttpGet("getbyconvention/{id}")]
        public ActionResult<IEnumerable<TalkDto>> GetByConvention(Guid id)
        {
            try
            {
                var existing = _conventionRepository.GetConvention(id);
                if (existing == null)
                {
                    return NotFound();
                }

                var talks = _talkRepository.GetTalks();
                var res = talks.Where(t => t.ConventionId == id).Select(t => t.AsDto(_userRepository, _conventionRepository));
                return StatusCode(200, res);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occurred while Registering user to the talk: {e.Message}");
            }
        }
    }
}
