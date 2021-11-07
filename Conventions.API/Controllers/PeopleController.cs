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
    }
}
