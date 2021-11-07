using Conventions.API.Dto;
using Conventions.API.Extensions;
using Conventions.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var person = _peopleRepository.GetPerson(id);
            if( person == null)
            {
                return NotFound();
            }

            return person.AsDto();
        }

        //POST /people
        [HttpPost]
        public ActionResult<PersonDto> CreatePerson(CreatePersonDto createPersonDto)
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

        //PUT /people/id
        [HttpPut("{id}")]
        public ActionResult UpdatePerson(Guid id, UpdatePersonDto updatePersonDto)
        {
            var existing = _peopleRepository.GetPerson(id);
            if(existing == null)
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

        //DELETE /people/id
        [HttpDelete("{id}")]
        public ActionResult DeletePerson(Guid id)
        {
            var existing = _peopleRepository.GetPerson(id);
            if (existing == null)
            {
                return NotFound();
            }

            _peopleRepository.DeletePerson(id);
            return NoContent();
        }
    }
}
