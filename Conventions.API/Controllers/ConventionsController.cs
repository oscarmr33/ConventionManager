using Conventions.API.Extensions;
using Conventions.API.Repositories;
using Conventions.API.Repositories.Interfaces;
using Conventions.Models.Dto;
using Conventions.Models.Entities;
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

        //GET /conventions
        [HttpGet]
        public IEnumerable<ConventionDto> GetConventions()
        {
            return  _conventionRepository.GetConventions().Select(convention => convention.AsDto(_peopleRepository));
        }

        //GET /conventions/id
        [HttpGet("{id}")]
        public ActionResult<ConventionDto> GetConvention(Guid id)
        {
            try
            {
                var convention = _conventionRepository.GetConvention(id);
                if (convention is null)
                {
                    return NotFound();
                }

                return convention.AsDto(_peopleRepository);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occured while Getting the convention: {e.Message}");
            }
        }

        //POST /conventions
        [HttpPost]
        public ActionResult<ConventionDto> CreateConvention(CreateConventionDto createConvention)
        {
            try
            {
                Convention convention = new()
                {
                    Id = Guid.NewGuid(),
                    Name = createConvention.Name,
                    StartDate = createConvention.StartDate,
                    EndDate = createConvention.EndDate,
                    Description = createConvention.Description,
                    AttendeesId = createConvention.AttendeesId.ToList(),
                    LocationsId = createConvention.LocationsId.ToList()
                };

                _conventionRepository.CreateConvention(convention);

                return CreatedAtAction(nameof(GetConvention), new { id = convention.Id }, convention.AsDto(_peopleRepository));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occured while Creating the convention: {e.Message}");
            }
        }
        
        //PUT /conventions/id
        [HttpPut("{id}")]
        public ActionResult UpdateConvention(Guid id, UpdateConventionDto updateConventionDto)
        {
            try
            {
                var existing = _conventionRepository.GetConvention(id);
                if (existing == null)
                {
                    return NotFound();
                }

                existing.Name = updateConventionDto.Name;
                existing.StartDate = updateConventionDto.StartDate;
                existing.EndDate = updateConventionDto.EndDate;
                existing.Description = updateConventionDto.Description;

                _conventionRepository.UpdateConvention(existing);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occured while Update the convention: {e.Message}");
            }
        }

        //DELETE /conventions/id
        [HttpDelete("{id}")]
        public ActionResult DeleteConvention(Guid id)
        {
            try
            {
                var existing = _conventionRepository.GetConvention(id);
                if (existing == null)
                {
                    return NotFound();
                }

                _conventionRepository.DeleteConvention(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occured while Deleting the convention: {e.Message}");
            }
        }
    }
}
