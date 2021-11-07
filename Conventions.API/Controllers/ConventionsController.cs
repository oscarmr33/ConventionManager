using Conventions.API.Entities;
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

        public ConventionsController(IConventionRepository conventionRepository)
        {
            _conventionRepository = conventionRepository;
        }

        [HttpGet]
        public IEnumerable<Convention> GetConventions()
        {
            return  _conventionRepository.GetConventions();
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
