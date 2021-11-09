using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Convention.Client.Models
{
	public class TalkDto
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public decimal LengthHours { get; set; }
        public PersonDto Speaker { get; set; }
        public ConventionDto Convention { get; set; }
        public List<PersonDto> Attendees { get; set; }
        public string LocationId { get; set; }
    }
}