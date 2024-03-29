﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Conventions.Models.Dto
{
    public class CreateTalkDto
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
        public string LocationId { get; set; }
    }
}
