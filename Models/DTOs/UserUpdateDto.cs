using System.ComponentModel.DataAnnotations;

namespace PointScore.Models
{
    public class UserUpdateDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        public string? FullName { get; set; } = string.Empty;
        public string? Organization { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        
        public string? CertificateThumbprint { get; set; } = string.Empty;
        
        public UserRole? Role { get; set; }
    }
}
