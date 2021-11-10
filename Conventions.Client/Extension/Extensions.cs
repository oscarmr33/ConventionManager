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
				Attendees = conventionDto.Attendees?.Select(personDto => personDto?.FromDto())?.ToList(),
			};
		}

		public static UserModel FromDto(this UserDto personDto)
		{
			return new UserModel()
			{
				Id = personDto.Id,
				Email = personDto.Email,
				Name = $"{personDto.FirstName} {personDto.LastName}",
				Telephone = personDto.Telephone
			};
		}

		public static TalkModel FromDto(this TalkDto talk)
		{
			return new TalkModel()
			{
				Id = talk.Id,
				Convention = talk.Convention?.FromDto(),
				Description = talk.Description,
				Name = talk.Name,
				LengthHours = talk.LengthHours,
				Speaker = talk.Speaker.FromDto(),
				StartDate = talk.StartDate,
				Attendees = talk.Attendees?.Select(personDto => personDto?.FromDto())?.ToList()
			};
		}
	}
}