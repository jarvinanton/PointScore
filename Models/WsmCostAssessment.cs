using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models;

[Table("WsmCostAssessments")]
public class WsmCostAssessment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid WsmRequestId { get; set; }

    [ForeignKey("WsmRequestId")]
    public WsmRequest WsmRequest { get; set; } = null!;

    // === COST INPUTS ===
    
    /// <summary>
    /// Coste de desarrollo
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal DevelopmentCost { get; set; }

    /// <summary>
    /// Coste de producción por unidad
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal ProductionCostPerUnit { get; set; }

    /// <summary>
    /// QTY de producción
    /// </summary>
    public int ProductionQuantity { get; set; }

    /// <summary>
    /// Coste TOTAL de producción (calculated or manual override)
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalProductionCost { get; set; }

    /// <summary>
    /// Instalación
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal InstallationCost { get; set; }

    // === CALCULATED OUTPUT ===
    
    /// <summary>
    /// Cost Score = Sum of all costs
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal CostScore { get; set; }

    /// <summary>
    /// Total WSM Cost = ProductionCostPerUnit * ProductionQuantity
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalWsmCost { get; set; }

    /// <summary>
    /// Cost Correlated Score = (WSMCompScore * TotalWsmCost) / 10,000,000
    /// </summary>
    [Column(TypeName = "decimal(18,4)")]
    public decimal CostCorrelatedScore { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
