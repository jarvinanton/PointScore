using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models
{
    public enum RoleRequestStatus
    {
        Pending,
        Approved,
        Denied
    }

    public class UserRoleRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public int RoleId { get; set; }       

        [Required]
        public UserRole RequestedRole { get; set; }

        [Required]
        public RoleRequestStatus Status { get; set; } = RoleRequestStatus.Pending;

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ProcessedAt { get; set; }

        public Guid? ProcessedBy { get; set; } // Admin que procesó la solicitud
        public virtual User? User { get; set; }
         public virtual Role? Role { get; set; } 
    }
}