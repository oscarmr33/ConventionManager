using Conventions.API.Entities;
using Conventions.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conventions.API.Repositories
{
    public class InMemConventionRepository : IConventionRepository
    {
        private IPeopleRepository _peopleRepository;

        public InMemConventionRepository(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
            PopulateConventions();
        }

        private List<Convention> _conventions = new()
        {
            new Convention 
            {
                Id = Guid.NewGuid(), 
                Name = "Spring Convention",
                Description = "The spring convention for the company",
                StartDate = new DateTimeOffset(2021,3,1,8,0,0,TimeSpan.Zero), 
                EndDate = new DateTimeOffset(2021, 3, 15, 17, 0, 0, TimeSpan.Zero),
                LocationsId = new List<string>() { "10-barrel-brewing-co-san-diego", "10-barrel-brewing-co-san-diego", "10-barrel-brewing-co-san-diego" }
            },
            new Convention
            {
                Id = Guid.NewGuid(),
                Name = "Summer Convention",
                Description = "The summer convention for the company",
                StartDate = new DateTimeOffset(2021, 7, 1, 8, 0, 0, TimeSpan.Zero),
                EndDate = new DateTimeOffset(2021, 7, 15, 17, 0, 0, TimeSpan.Zero),
                LocationsId = new List<string>() { "10-barrel-brewing-co-san-diego", "10-barrel-brewing-co-san-diego", "10-barrel-brewing-co-san-diego" }
            },
            new Convention
            {
                Id = Guid.NewGuid(),
                Name = "Winter Convention",
                Description = "The winter convention for the company",
                StartDate = new DateTimeOffset(2021, 12, 1, 8, 0, 0, TimeSpan.Zero),
                EndDate = new DateTimeOffset(2021, 12, 15, 17, 0, 0, TimeSpan.Zero),
                LocationsId = new List<string>() { "10-barrel-brewing-co-san-diego", "10-barrel-brewing-co-san-diego", "10-barrel-brewing-co-san-diego" }
            }
        };
        
        public IEnumerable<Convention> GetConventions()
        {
            return _conventions;
        }

        public Convention GetConvention(Guid id)
        {
            return _conventions.SingleOrDefault(convention => convention.Id == id);
        }

        //Ugly method to populate the conventions with the current people, this shouldnt have to exists on a DB solution
        private void PopulateConventions()
        {
            _conventions[0].Attendees = _peopleRepository.GetPeople();
            _conventions[1].Attendees = _peopleRepository.GetPeople();
            _conventions[2].Attendees = _peopleRepository.GetPeople();
        }
    }
}
