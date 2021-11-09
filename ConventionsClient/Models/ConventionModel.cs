using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConventionsClient.Models
{
	public class ConventionModel
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public List<LocationModel> Locations { get; set; } = new List<LocationModel>();
        public List<PersonModel> Attendees { get; set; } = new List<PersonModel>();
    }
}