using Conventions.API.Dto;
using Conventions.API.Entities;
using Conventions.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                Attendees = peopleRepository.GetPeople().Where(person => convention.AttendeesId.Contains(person.Id)).Select(person => person)
            };
        }
    }
}
