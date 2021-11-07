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
    public class TalksController : ControllerBase
    {
        private readonly ITalksRepository _talkRepository;

        public TalksController(ITalksRepository talksRepository)
        {
            //_inMemPeopleRepository = peopleRepository;
        }

        [HttpGet]
        public IEnumerable<Talk> GetTalks()
        {
            return _talkRepository.GetTalks();
        }

        [HttpGet("{id}")]
        public ActionResult<Talk> Gettalk(Guid id)
        {
            var talk = _talkRepository.GetTalk(id);
            if(talk is null)
            {
                return NotFound();
            }

            return talk;
        }
    }
}
