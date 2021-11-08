﻿using Conventions.API.Extensions;
using Conventions.API.Repositories;
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
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleController(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        //GET /people
        [HttpGet]
        public IEnumerable<PersonDto> GetPeople()
        {
            return _peopleRepository.GetPeople().Select(person => person.AsDto());
        }

        //GET /people/id
        [HttpGet("{id}")]
        public ActionResult<PersonDto> GetPerson(Guid id)
        {
            try
            {
                var person = _peopleRepository.GetPerson(id);
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
        public ActionResult<PersonDto> CreatePerson(CreatePersonDto createPersonDto)
        {
            try
            {
                Person person = new()
                {
                    Id = Guid.NewGuid(),
                    FirstName = createPersonDto.FirstName,
                    LastName = createPersonDto.LastName,
                    Email = createPersonDto.Email,
                    Telephone = createPersonDto.Telephone
                };

                _peopleRepository.CreatePerson(person);

                return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person.AsDto());
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occured while Creating the person: {e.Message}");
            }
        }

        //PUT /people/id
        [HttpPut("{id}")]
        public ActionResult UpdatePerson(Guid id, UpdatePersonDto updatePersonDto)
        {
            try
            {
                var existing = _peopleRepository.GetPerson(id);
                if (existing == null)
                {
                    return NotFound();
                }

                existing.FirstName = updatePersonDto.FirstName;
                existing.LastName = updatePersonDto.LastName;
                existing.Telephone = updatePersonDto.Telephone;
                existing.Email = updatePersonDto.Email;

                _peopleRepository.UpdatePerson(existing);

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
                var existing = _peopleRepository.GetPerson(id);
                if (existing == null)
                {
                    return NotFound();
                }

                _peopleRepository.DeletePerson(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occured while Deleting the person: {e.Message}");
            }
        }
    }
}
