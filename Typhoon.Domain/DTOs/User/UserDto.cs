using System.ComponentModel.DataAnnotations;

namespace Typhoon.Domain.DTOs.User
{
    public class UserDto : LoginDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
