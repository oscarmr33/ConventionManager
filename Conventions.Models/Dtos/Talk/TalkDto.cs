using Conventions.Models.Entities;
using System;
using System.Collections.Generic;

namespace Conventions.Models.Dto
{
    public class TalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public decimal LengthHours { get; set; }
        public User Speaker { get; set; }
        public Convention Convention { get; set; }
        public List<User> Attendees { get; set; }
        public string LocationId { get; set; }
    }
}
