using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Convention.Client.Models
{
	public class ConventionModel
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public List<LocationModel> Locations { get; set; } = new List<LocationModel>();
        public List<UserModel> Attendees { get; set; } = new List<UserModel>();
    }
}