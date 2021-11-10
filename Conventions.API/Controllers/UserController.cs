using Conventions.API.Extensions;
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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _peopleRepository;
        private readonly IConventionRepository _conventionRepository;
        private readonly ITalksRepository _talksRepository;

        public UserController(IUserRepository peopleRepository, IConventionRepository conventionRepository, ITalksRepository talksRepository)
        {
            _peopleRepository = peopleRepository;
            _conventionRepository = conventionRepository;
            _talksRepository = talksRepository;
        }

        //GET /people
        [HttpGet]
        public IEnumerable<UserDto> GetPeople()
        {
            return _peopleRepository.GetUsers().Select(person => person.AsDto());
        }

        //GET /people/id
        [HttpGet("{id}")]
        public ActionResult<UserDto> GetPerson(Guid id)
        {
            try
            {
                var person = _peopleRepository.GetUser(id);
                if (person == null)
                {
                    return NotFound();
                }

                return person.AsDto();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occured while Getting the person: {e.Message}");
            }
        }

        //POST /people
        [HttpPost]
        public ActionResult<UserDto> CreatePerson(CreateUserDto createPersonDto)
        {
            try
            {
                User person = new()
                {
                    Id = Guid.NewGuid(),
                    FirstName = createPersonDto.FirstName,
                    LastName = createPersonDto.LastName,
                    Email = createPersonDto.Email,
                    Telephone = createPersonDto.Telephone
                };

                _peopleRepository.CreateUser(person);

                return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person.AsDto());
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occured while Creating the person: {e.Message}");
            }
        }

        //PUT /people/id
        [HttpPut("{id}")]
        public ActionResult UpdatePerson(Guid id, UpdateUserDto updatePersonDto)
        {
            try
            {
                var existing = _peopleRepository.GetUser(id);
                if (existing == null)
                {
                    return NotFound();
                }

                existing.FirstName = updatePersonDto.FirstName;
                existing.LastName = updatePersonDto.LastName;
                existing.Telephone = updatePersonDto.Telephone;
                existing.Email = updatePersonDto.Email;

                _peopleRepository.UpdateUser(existing);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occured while updating the person: {e.Message}");
            }
        }

        //DELETE /people/id
        [HttpDelete("{id}")]
        public ActionResult DeletePerson(Guid id)
        {
            try
            {
                var existing = _peopleRepository.GetUser(id);
                if (existing == null)
                {
                    return NotFound();
                }

                _peopleRepository.DeleteUser(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occured while Deleting the person: {e.Message}");
            }
        }

		[HttpGet("register/{id}")]
		public ActionResult RegisterToConvention(Guid id, Guid conventionId)
		{
			try
			{
				var existingConvention = _conventionRepository.GetConvention(conventionId);
				if (existingConvention == null)
				{
					return StatusCode(404, "Convention not found");
				}
				if (_peopleRepository.GetUser(id) == null)
				{
					return NotFound();
				}

				existingConvention.AttendeesId.Add(id);

				return NoContent();
			}
			catch (Exception e)
			{
				return StatusCode(500, $"An error has occured while Deleting the person: {e.Message}");
			}
		}

		[HttpGet("talks/{id}")]
		public ActionResult<IEnumerable<TalkDto>> GetPersonTalks(Guid id)
		{
			try
			{
				if (_peopleRepository.GetUser(id) == null)
				{
					return NotFound();
				}

				return StatusCode(200, _talksRepository.GetTalks().Where(talk => talk.SpeakerId == id).Select(talk => talk.AsDto(_peopleRepository, _conventionRepository)));
			}
			catch (Exception e)
			{
				return StatusCode(500, $"An error has occured while Getting the talks: {e.Message}");
			}
		}
	}
}
