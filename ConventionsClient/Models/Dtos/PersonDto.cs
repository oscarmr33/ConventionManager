﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConventionsClient.Models
{
	public class PersonDto
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Telephone { get; set; }
	}
}