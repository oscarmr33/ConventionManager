using Conventions.API.Entities;
using Conventions.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conventions.API.Repositories
{
    public class InMenTalksRepository : ITalksRepository
    {
        private readonly List<Talk> _talks = new()
        {
            new Talk()
            {
                Id = Guid.NewGuid(),
                Name = "First talk Spring",
                Description = "First talk ok the spring convention",
                SpeakerId = new Guid("7b80daf0-3cca-446a-bb21-f8036761d115"),
                StartDate = new DateTimeOffset(2021, 3, 1, 9, 0, 0, TimeSpan.Zero),
                LocationId = "10-barrel-brewing-co-san-diego",
                ConventionId = new Guid("48988684-c145-4a2d-8fa9-b3ff2fa6f21b"),
                LengthHours = 1,
                AttendeesId = new List<Guid>()
                {
                    new Guid("0e2118b1-ba06-4145-a42b-51dd710c44d0"),
                    new Guid("74155be9-fd2a-4d34-8cc1-b82169d7d97b")
                }
            }
        };

        public IEnumerable<Talk> GetTalks()
        {
            return _talks;
        }
        
        public Talk GetTalk(Guid id)
        {
            return _talks.SingleOrDefault(talk => talk.Id == id);
        }

        public void CreateTalk(Talk talk)
        {
            _talks.Add(talk);
        }

        public void UpdateTalk(Talk talk)
        {
            var index = _talks.FindIndex(existingTalk => existingTalk.Id == talk.Id);
            _talks[index] = talk;
        }

        public void DeleteTalk(Guid id)
        {
            var index = _talks.FindIndex(existingTalk => existingTalk.Id == id);
            _talks.RemoveAt(index);
        }
    }
}
