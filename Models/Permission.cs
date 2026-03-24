using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models
{
   public class Permission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PermissionId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Resource { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Action { get; set; } = string.Empty;

    [NotMapped]
    public string FullName => $"{Resource}:{Action}";

    [MaxLength(500)]
    public string? Description { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    // public ICollection<RolePermission> Roles { get; set; } = new List<RolePermission>();
}

}
