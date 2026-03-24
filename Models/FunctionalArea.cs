using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models;
public class FunctionalArea
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;  // Logistics, Safety, Software, etc.

    public decimal Weight { get; set; }  // 10%, 5%, etc. (como decimal 0.10, 0.05)

    public bool IsActive { get; set; } = true;

    [MaxLength(100)]
    public string? DefaultRoleName { get; set; }  // Nombre del rol por defecto que tiene acceso

    public ICollection<WsmFunctionalImpact> WsmImpacts { get; set; } = new List<WsmFunctionalImpact>();
    public ICollection<DetailedSiaSection> Sections { get; set; } = new List<DetailedSiaSection>();
}