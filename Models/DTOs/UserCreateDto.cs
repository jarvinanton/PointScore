using System.ComponentModel.DataAnnotations;

namespace PointScore.Models
{
    public class UserCreateDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Organization { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(100, MinimumLength = 8)]
        public string? Password { get; set; }

        [StringLength(100, MinimumLength = 6)]
        public string? CertificateThumbprint { get; set; }

        // public UserRole? Role { get; set; }
    }
}
