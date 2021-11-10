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
        public UserModel Speaker { get; set; }
        public ConventionModel Convention { get; set; }
        public List<UserModel> Attendees { get; set; }
        public LocationModel Location { get; set; }
    }
}