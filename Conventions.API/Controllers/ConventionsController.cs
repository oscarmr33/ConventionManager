using Conventions.API.Dto;
using Conventions.API.Entities;
using Conventions.API.Extensions;
using Conventions.API.Repositories;
using Conventions.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conventions.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConventionsController : ControllerBase
    {
        private readonly IConventionRepository _conventionRepository;
        private readonly IPeopleRepository _peopleRepository;

        public ConventionsController(IConventionRepository conventionRepository, IPeopleRepository peopleRepository)
        {
            _conventionRepository = conventionRepository;
            _peopleRepository = peopleRepository;
        }

        [HttpGet]
        public IEnumerable<ConventionDto> GetConventions()
        {
            return  _conventionRepository.GetConventions().Select(convention => convention.AsDto(_peopleRepository));
        }

        [HttpGet("{id}")]
        public ActionResult<Convention> GetConvention(Guid id)
        {
            var convention = _conventionRepository.GetConvention(id);
            if(convention is null)
            {
                return NotFound();
            }

            return convention;
        }
    }
}
