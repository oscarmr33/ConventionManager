using Convention.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Convention.Client.Extension
{
	public static class Extensions
	{
		public static ConventionModel FromDto(this ConventionDto conventionDto)
		{
			return new ConventionModel()
			{
				Id = conventionDto.Id,
				Description = conventionDto.Description,
				Name = conventionDto.Name,
				EndDate = conventionDto.EndDate,
				StartDate = conventionDto.StartDate,
				Attendees = conventionDto.Attendees.Select(personDto => personDto.FromDto()).ToList(),
			};
		}

		public static PersonModel FromDto(this PersonDto personDto)
		{
			return new PersonModel()
			{
				Id = personDto.Id,
				Email = personDto.Email,
				Name = $"{personDto.FirstName} {personDto.LastName}",
				Telephone = personDto.Telephone
			};
		}
	}
}