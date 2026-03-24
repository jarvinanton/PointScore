using System.ComponentModel.DataAnnotations;
using PointScore.Models;

namespace PointScore.Models.DTOs
{
    public class CACRegisterDto
    {
        [Required]
        [MaxLength(100)]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }

    public class CACLoginResponseDto
    {
        public string? Token { get; set; }
        public UserDto? User { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
    }
}
