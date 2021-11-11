using Conventions.API.Repositories.Interfaces;
using Conventions.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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
            },
            new Talk()
            {
                Id = Guid.NewGuid(),
                Name = "Second talk Spring",
                Description = "Second talk ok the spring convention",
                SpeakerId = new Guid("0e2118b1-ba06-4145-a42b-51dd710c44d0"),
                StartDate = new DateTimeOffset(2021, 3, 2, 9, 0, 0, TimeSpan.Zero),
                LocationId = "12-gates-brewing-company-williamsville",
                ConventionId = new Guid("48988684-c145-4a2d-8fa9-b3ff2fa6f21b"),
                LengthHours = 1,
                AttendeesId = new List<Guid>()
                {
                    new Guid("7b80daf0-3cca-446a-bb21-f8036761d115"),
                    new Guid("74155be9-fd2a-4d34-8cc1-b82169d7d97b")
                }
            },
            new Talk()
            {
                Id = Guid.NewGuid(),
                Name = "Third talk Spring",
                Description = "Last talk ok the spring convention",
                SpeakerId = new Guid("74155be9-fd2a-4d34-8cc1-b82169d7d97b"),
                StartDate = new DateTimeOffset(2021, 3, 3, 9, 0, 0, TimeSpan.Zero),
                LocationId = "12-west-brewing-company-production-facility-mesa",
                ConventionId = new Guid("48988684-c145-4a2d-8fa9-b3ff2fa6f21b"),
                LengthHours = 1,
                AttendeesId = new List<Guid>()
                {
                    new Guid("0e2118b1-ba06-4145-a42b-51dd710c44d0"),
                    new Guid("7b80daf0-3cca-446a-bb21-f8036761d115")
                }
            },
            new Talk()
            {
                Id = Guid.NewGuid(),
                Name = "First talk Summer",
                Description = "First talk ok the summer convention",
                SpeakerId = new Guid("7b80daf0-3cca-446a-bb21-f8036761d115"),
                StartDate = new DateTimeOffset(2021, 7, 1, 9, 0, 0, TimeSpan.Zero),
                LocationId = "10-barrel-brewing-co-denver-denver",
                ConventionId = new Guid("7d167f30-7bad-4831-9c34-42b9c8556cb5"),
                LengthHours = 1,
                AttendeesId = new List<Guid>()
                {
                    new Guid("0e2118b1-ba06-4145-a42b-51dd710c44d0")
                }
            },
            new Talk()
            {
                Id = Guid.NewGuid(),
                Name = "First talk Winter",
                Description = "First talk ok the winter convention",
                SpeakerId = new Guid("0e2118b1-ba06-4145-a42b-51dd710c44d0"),
                StartDate = new DateTimeOffset(2021, 11, 1, 9, 0, 0, TimeSpan.Zero),
                LocationId = "10-barrel-brewing-co-bend-pub-bend",
                ConventionId = new Guid("85e06ccc-5c5f-4e3d-806f-344c2f13fe07"),
                LengthHours = 1,
                AttendeesId = new List<Guid>()
                {
                    new Guid("74155be9-fd2a-4d34-8cc1-b82169d7d97b"),
                    new Guid("0e2118b1-ba06-4145-a42b-51dd710c44d0"),
                    new Guid("7b80daf0-3cca-446a-bb21-f8036761d115")
                }
            },
            new Talk()
            {
                Id = Guid.NewGuid(),
                Name = "Last talk Winter",
                Description = "Last talk ok the winter convention",
                SpeakerId = new Guid("74155be9-fd2a-4d34-8cc1-b82169d7d97b"),
                StartDate = new DateTimeOffset(2021, 12, 3, 9, 0, 0, TimeSpan.Zero),
                LocationId = "10-barrel-brewing-co-san-diego",
                ConventionId = new Guid("85e06ccc-5c5f-4e3d-806f-344c2f13fe07"),
                LengthHours = 1,
                AttendeesId = new List<Guid>()
                {
                    new Guid("7b80daf0-3cca-446a-bb21-f8036761d115"),
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
