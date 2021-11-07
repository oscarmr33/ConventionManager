using System;

namespace Conventions.Models
{
    /// <summary>
    /// Class that represents the conventions, it includes a 
    /// </summary>
    public class Convention
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public DateTimeOffset StartDate { get; init; }
        public DateTimeOffset EndDate { get; init; }
        
    }
}
