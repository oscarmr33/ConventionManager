using System;

namespace Conventions.API.Dto
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        /// <summary>
        /// We are considering a single Email for this example, but usually this would be better on list to allow multiple per user.
        /// </summary>
        public string Email { get; set; }
        public string Telephone { get; set; }
    }
}
