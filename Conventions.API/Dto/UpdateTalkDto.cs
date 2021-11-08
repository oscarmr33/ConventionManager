using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Conventions.API.Dto
{
	public class UpdateTalkDto
	{
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTimeOffset StartDate { get; set; }
        [Required]
        public decimal LengthHours { get; set; }
        [Required]
        public Guid SpeakerId { get; set; }
        [Required]
        public Guid ConventionId { get; set; }
        public List<Guid> AttendeesId { get; set; }
        [Required]
        public string LocationId { get; set; }
    }
}
