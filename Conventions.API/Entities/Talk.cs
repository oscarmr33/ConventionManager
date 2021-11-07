using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conventions.API.Entities
{
    public class Talk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public int LengthHours { get; set; }
        public Person Speaker { get; set; }
        public Convention Convention { get; set; }
        public List<Person> Attendees { get; set; } = new List<Person>();
        public string LocationId { get; set; }
    }
}
