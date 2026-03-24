using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models;

[Table("ScoringFormulaWeights")]
public class ScoringFormulaWeights
{
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Technical Complexity Weight (Column D) - Default: 15%
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal TechnicalWeight { get; set; } = 15.00m;

    /// <summary>
    /// Functional Support Impact Weight (Column H) - Default: 15%
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal FunctionalWeight { get; set; } = 15.00m;

    /// <summary>
    /// Schedule Duration Weight (Column L) - Default: 20%
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal ScheduleWeight { get; set; } = 20.00m;

    /// <summary>
    /// User/Operational Impact Weight (Column P) - Default: 50%
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal UserImpactWeight { get; set; } = 50.00m;

    /// <summary>
    /// Cost Weight (Column T) - Default: 75%
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal CostWeight { get; set; } = 75.00m;

    /// <summary>
    /// Only one set of weights can be active at a time
    /// </summary>
    [Required]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// User who made the last change (Program Manager, Admin, Config Manager)
    /// </summary>
    public Guid? UpdatedBy { get; set; }

    [ForeignKey("UpdatedBy")]
    public User? UpdatedByUser { get; set; }

    /// <summary>
    /// When the weights were last updated
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Reason for the weight change
    /// </summary>
    [MaxLength(500)]
    public string? Comments { get; set; }
}
