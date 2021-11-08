﻿using Conventions.API.Extensions;
using Conventions.API.Repositories;
using Conventions.API.Repositories.Interfaces;
using Conventions.Models.Dto;
using Conventions.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Conventions.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TalksController : ControllerBase
    {
        private readonly ITalksRepository _talkRepository;
        private readonly IConventionRepository _conventionRepository;
        private readonly IPeopleRepository _peopleRepository;

        public TalksController(ITalksRepository talksRepository, IConventionRepository conventionRepository, IPeopleRepository peopleRepository)
        {
            _talkRepository = talksRepository;
            _conventionRepository = conventionRepository;
            _peopleRepository = peopleRepository;
        }

        [HttpGet]
        public IEnumerable<TalkDto> GetTalks()
        {
            return _talkRepository.GetTalks().Select(talk => talk.AsDto(_peopleRepository, _conventionRepository));
        }

        [HttpGet("{id}")]
        public ActionResult<TalkDto> Gettalk(Guid id)
        {
            try
            {
                var talk = _talkRepository.GetTalk(id);
                if (talk is null)
                {
                    return NotFound();
                }

                return talk.AsDto(_peopleRepository, _conventionRepository);
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

                if (_peopleRepository.GetPerson(createTalk.SpeakerId) == null)
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

                return CreatedAtAction(nameof(Gettalk), new { id = talk.Id }, talk.AsDto(_peopleRepository, _conventionRepository));
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

                if (_peopleRepository.GetPerson(updateTalkDto.SpeakerId) == null)
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

        //POST /talks/id&attendeeId
        [HttpPost("{id}")]
        public ActionResult RegisterToTalk(Guid id, Guid attendeeId)
		{
            var existing = _talkRepository.GetTalk(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (_peopleRepository.GetPerson(attendeeId) == null)
            {
                return StatusCode(404, "Attendee not found");
            }

            existing.AttendeesId.Add(attendeeId);

            return NoContent();
        }
    }
}
