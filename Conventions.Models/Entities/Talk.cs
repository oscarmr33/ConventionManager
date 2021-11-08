using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conventions.Models.Entities
{
    public class Talk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public decimal LengthHours { get; set; }
        public Guid SpeakerId { get; set; }
        public Guid ConventionId { get; set; }
        public List<Guid> AttendeesId { get; set; }
        public string LocationId { get; set; }
    }
}
