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
                LocationsId = new List<string>() { "10-barrel-brewing-co-san-diego", "10-barrel-brewing-co-san-diego", "10-barrel-brewing-co-san-diego" },
                AttendeesId = new List<Guid>()
                {
                    Guid.Parse("7b80daf0-3cca-446a-bb21-f8036761d115"),
                    Guid.Parse("74155be9-fd2a-4d34-8cc1-b82169d7d97b"),
                    Guid.Parse("0e2118b1-ba06-4145-a42b-51dd710c44d0")
                }
            },
            new Convention
            {
                Id = Guid.NewGuid(),
                Name = "Summer Convention",
                Description = "The summer convention for the company",
                StartDate = new DateTimeOffset(2021, 7, 1, 8, 0, 0, TimeSpan.Zero),
                EndDate = new DateTimeOffset(2021, 7, 15, 17, 0, 0, TimeSpan.Zero),
                LocationsId = new List<string>() { "10-barrel-brewing-co-san-diego", "10-barrel-brewing-co-san-diego", "10-barrel-brewing-co-san-diego" },
                AttendeesId = new List<Guid>()
                {
                    Guid.Parse("7b80daf0-3cca-446a-bb21-f8036761d115"),
                    Guid.Parse("0e2118b1-ba06-4145-a42b-51dd710c44d0")
                }
            },
            new Convention
            {
                Id = Guid.NewGuid(),
                Name = "Winter Convention",
                Description = "The winter convention for the company",
                StartDate = new DateTimeOffset(2021, 12, 1, 8, 0, 0, TimeSpan.Zero),
                EndDate = new DateTimeOffset(2021, 12, 15, 17, 0, 0, TimeSpan.Zero),
                LocationsId = new List<string>() { "10-barrel-brewing-co-san-diego", "10-barrel-brewing-co-san-diego", "10-barrel-brewing-co-san-diego" },
                AttendeesId = new List<Guid>()
                {
                    Guid.Parse("7b80daf0-3cca-446a-bb21-f8036761d115"),
                    Guid.Parse("74155be9-fd2a-4d34-8cc1-b82169d7d97b")
                }
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
    }
}
