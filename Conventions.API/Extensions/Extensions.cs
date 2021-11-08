using Conventions.API.Repositories;
using Conventions.API.Repositories.Interfaces;
using Conventions.Models.Dto;
using Conventions.Models.Entities;
using System;
using System.Linq;

namespace Conventions.API.Extensions
{
    public static class Extensions
    {
        public static PersonDto AsDto(this Person person)
        {
            return new PersonDto()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                Telephone = person.Telephone
            };
        }

        public static ConventionDto AsDto(this Convention convention, IPeopleRepository peopleRepository)
        {
            return new ConventionDto()
            {
                Id = convention.Id,
                Description = convention.Description,
                Name = convention.Name,
                StartDate = convention.StartDate,
                EndDate = convention.EndDate,
                LocationsId = convention.LocationsId,
                Attendees = peopleRepository.GetPeople().Where(person => convention.AttendeesId.Contains(person.Id))
            };
        }

        public static TalkDto AsDto(this Talk talk, IPeopleRepository peopleRepository, IConventionRepository convetionRepository)
        {
            return new TalkDto()
            {
                Id = talk.Id,
                Name = talk.Name,
                Description = talk.Description,
                LengthHours = talk.LengthHours,
                StartDate = talk.StartDate,
                LocationId = talk.LocationId,
                Speaker = peopleRepository.GetPerson(talk.SpeakerId),
                Convention = convetionRepository.GetConvention(talk.ConventionId),
                Attendees = peopleRepository.GetPeople().Where(person => talk.AttendeesId.Contains(person.Id)).ToList()
            };
        }
    }
}
