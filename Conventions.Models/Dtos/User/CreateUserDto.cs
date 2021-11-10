using System.ComponentModel.DataAnnotations;

namespace Conventions.Models.Dto
{
    public class CreateUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Telephone { get; set; }
    }
}
