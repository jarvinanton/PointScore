using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models
{
   public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        // Relación: un rol tiene muchos permisos
        // public ICollection<RolePermission> Permissions { get; set; } = new List<RolePermission>();

        // Relación: usuarios asignados a este rol
        public virtual ICollection<UserRoleMap> UserRoles { get; set; } = new List<UserRoleMap>();
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }

}
