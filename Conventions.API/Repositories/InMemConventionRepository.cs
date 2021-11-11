using Conventions.API.Repositories.Interfaces;
using Conventions.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Conventions.API.Repositories
{
    public class InMemConventionRepository : IConventionRepository
    {
        private List<Convention> _conventions = new()
        {
            new Convention 
            {
                Id = new Guid("48988684-c145-4a2d-8fa9-b3ff2fa6f21b"), 
                Name = "Spring Convention",
                Description = "The spring convention for the company",
                StartDate = new DateTimeOffset(2021,3,1,8,0,0,TimeSpan.Zero), 
                EndDate = new DateTimeOffset(2021, 3, 15, 17, 0, 0, TimeSpan.Zero),
                LocationsId = new List<string>() { "10-barrel-brewing-co-san-diego", "12-west-brewing-company-production-facility-mesa", "12-gates-brewing-company-williamsville" },
                AttendeesId = new List<Guid>()
                {
                    Guid.Parse("7b80daf0-3cca-446a-bb21-f8036761d115"),
                    Guid.Parse("74155be9-fd2a-4d34-8cc1-b82169d7d97b"),
                    Guid.Parse("0e2118b1-ba06-4145-a42b-51dd710c44d0")
                }
            },
            new Convention
            {
                Id = new Guid("7d167f30-7bad-4831-9c34-42b9c8556cb5"),
                Name = "Summer Convention",
                Description = "The summer convention for the company",
                StartDate = new DateTimeOffset(2021, 7, 1, 8, 0, 0, TimeSpan.Zero),
                EndDate = new DateTimeOffset(2021, 7, 15, 17, 0, 0, TimeSpan.Zero),
                LocationsId = new List<string>() { "10-barrel-brewing-co-san-diego",  "10-barrel-brewing-co-bend-1" },
                AttendeesId = new List<Guid>()
                {
                    Guid.Parse("7b80daf0-3cca-446a-bb21-f8036761d115"),
                    Guid.Parse("0e2118b1-ba06-4145-a42b-51dd710c44d0")
                }
            },
            new Convention
            {
                Id = new Guid("85e06ccc-5c5f-4e3d-806f-344c2f13fe07"),
                Name = "Winter Convention",
                Description = "The winter convention for the company",
                StartDate = new DateTimeOffset(2021, 12, 1, 8, 0, 0, TimeSpan.Zero),
                EndDate = new DateTimeOffset(2021, 12, 15, 17, 0, 0, TimeSpan.Zero),
                LocationsId = new List<string>() { "10-barrel-brewing-co-bend-pub-bend", "10-barrel-brewing-co-denver-denver", "10-barrel-brewing-co-san-diego" },
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

        public void CreateConvention(Convention convention)
        {
            _conventions.Add(convention);
        }

        public void UpdateConvention(Convention convention)
        {
            var index = _conventions.FindIndex(existingConvention => existingConvention.Id == convention.Id);
            _conventions[index] = convention;
        }

        public void DeleteConvention(Guid id)
        {
            var index = _conventions.FindIndex(existingConvention => existingConvention.Id == id);
            _conventions.RemoveAt(index);
        }
    }
}
