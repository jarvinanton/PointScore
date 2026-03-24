using System.ComponentModel.DataAnnotations;

namespace PointScore.Models
{
    public class UserDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Organization { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(100, MinimumLength = 6)]
        public List<UserRoleMap>? Roles { get; set; }

        // public UserRole? Role { get; set; }
    }
    public class UserRegisterDto
    {
        public string Email { get; set; } = null!;
        
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Organization { get; set; } = null!;
    }

}
