using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conventions.Models.Entities
{
    public class Convention
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        /// <summary>
        /// The locations id needs to be a string due to the nature of the ids from the breweries api
        /// </summary>
        public IEnumerable<string> LocationsId { get; set; }
        public IEnumerable<Guid> AttendeesId { get; set; }
    }
}
