using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Convention.Client.Models
{
	public class PersonModel
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
    }
}