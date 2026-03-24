using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models;
public class DetailedSiaSection
{
    [Key]
    public int Id { get; set; }

    public int FunctionalAreaId { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = null!;  // "Facilities & Infrastructure", "Training & Training Support", etc.

    public int Order { get; set; }  // Orden exacto en la hoja

    public bool IsActive { get; set; } = true;

    [Column(TypeName = "decimal(18,6)")]
    public decimal MaxCostWeight { get; set; } = 0;

    [Column(TypeName = "decimal(18,6)")]
    public decimal MaxScheduleWeight { get; set; } = 0;

    [Column(TypeName = "decimal(18,6)")]
    public decimal MaxPerformanceWeight { get; set; } = 0;

    public FunctionalArea FunctionalArea { get; set; } = null!;

    public ICollection<WsmDetailedSiaResponse> Responses { get; set; } = new List<WsmDetailedSiaResponse>();
}