using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Convention.Client.Models
{
	public class TalkModel
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public decimal LengthHours { get; set; }
        public PersonModel Speaker { get; set; }
        public PersonModel Convention { get; set; }
        public List<PersonModel> Attendees { get; set; }
        public string LocationId { get; set; }
    }
}