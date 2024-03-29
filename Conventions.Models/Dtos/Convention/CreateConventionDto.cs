﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Conventions.Models.Dto
{
    public class CreateConventionDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTimeOffset StartDate { get; set; }
        [Required]
        public DateTimeOffset EndDate { get; set; }
        public IEnumerable<string> LocationsId { get; set; }
        public IEnumerable<Guid> AttendeesId { get; set; }
    }
}
