using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PointScore.Models
{
   public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [MaxLength(150)]
        public string? Organization { get; set; }

        [MaxLength(100)]
        public string? CertificateThumbprint { get; set; } = string.Empty;

        public bool isActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ❗Este enum queda como auxiliar solo para la creación del usuario
        // public UserRole DefaultRole { get; set; } = UserRole.BasicUser;

        // 🔥 Nueva relación M:N con Role
         public virtual ICollection<UserRoleMap> UserRoles { get; set; } = new List<UserRoleMap>();
        // public ICollection<UserRoleMap> Roles { get; set; } = new List<UserRoleMap>();

        public Guid? DesignOwnerId { get; set; }

        [ForeignKey("DesignOwnerId")]
        public virtual RecommendedDesignOwner? DesignOwner { get; set; }

        // ---- Authentication properties ----
        [JsonIgnore]
        public byte[] PasswordHash { get; set; } = [];

        [JsonIgnore]
        public byte[] PasswordSalt { get; set; } = [];

        public string? TwoFactorSecret { get; set; }

        public bool TwoFactorEnabled { get; set; } = false;

        [NotMapped]
        [JsonIgnore]
        public string? TwoFactorRecoveryCode { get; set; }
        public virtual ICollection<UserRoleRequest> RoleRequests { get; set; } = new List<UserRoleRequest>();

    }
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
        
    }

   public class AuthResponse
    {
        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }
        public bool RequiresTwoFactor { get; set; }
        public string? Message { get; set; }
        public string? UserRole { get; set; }  // Cambiado a string para devolver el nombre del rol
        public bool TwoFactorEnabled { get; set; }
    }

}