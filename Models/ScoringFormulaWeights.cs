using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models;

[Table("ScoringFormulaWeights")]
public class ScoringFormulaWeights
{
    [Key]
    public int Id { get; set; }

    // ============================================================
    // TOP-LEVEL CATEGORY WEIGHTS
    // ============================================================

    /// <summary>Technical Complexity Weight - Default: 40%</summary>
    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal TechnicalWeight { get; set; } = 40.00m;

    /// <summary>Functional Support Impact Weight - Default: 30%</summary>
    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal FunctionalWeight { get; set; } = 30.00m;

    /// <summary>Schedule Duration Weight - Default: 30%</summary>
    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal ScheduleWeight { get; set; } = 30.00m;

    /// <summary>User/Operational Impact Weight - Default: 100%</summary>
    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal UserImpactWeight { get; set; } = 100.00m;

    /// <summary>Cost Weight - Default: 75%</summary>
    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal CostWeight { get; set; } = 75.00m;

    // ============================================================
    // TECHNICAL SUB-WEIGHTS (must sum to 100)
    // ============================================================

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Tech_ENG_SIA_WT { get; set; } = 5.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Tech_TECH_IMP_WT { get; set; } = 15.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Tech_TRL_WT { get; set; } = 25.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Tech_DELIV_WT { get; set; } = 10.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Tech_SRR_PDR_CDR_WT { get; set; } = 10.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Tech_INTERD_WT { get; set; } = 10.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Tech_SELFDEP_WT { get; set; } = 25.00m;

    // ============================================================
    // FUNCTIONAL SUB-WEIGHTS (must sum to 100)
    // ============================================================

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal FntL_SIA_WT { get; set; } = 30.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal FntL_IMP_WT { get; set; } = 70.00m;

    // ============================================================
    // USER SUB-WEIGHTS (must sum to 100)
    // ============================================================

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal User_ORI_WT { get; set; } = 25.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal User_USER_IMP_WT { get; set; } = 40.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal User_TIME_CRIT_WT { get; set; } = 35.00m;

    // ============================================================
    // SCHEDULE SUB-WEIGHTS (must sum to 100)
    // ============================================================

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Sch_SCH_IMP_WT { get; set; } = 60.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Sch_MRL_WT { get; set; } = 40.00m;

    // ============================================================
    // COST SUB-WEIGHTS (must sum to 100)
    // ============================================================

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Cost_SIA_TECH_ECP_WT { get; set; } = 5.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Cost_TRL_WT { get; set; } = 5.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Cost_TECH_IMP_WT { get; set; } = 5.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Cost_DELIV_WT { get; set; } = 15.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Cost_MRL_WT { get; set; } = 5.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Cost_SRR_PDR_CDR_WT { get; set; } = 15.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Cost_INTERD_WT { get; set; } = 10.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Cost_SELFDEP_WT { get; set; } = 5.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Cost_TIME_CRIT_WT { get; set; } = 10.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Cost_SIA_COST_ECP_WT { get; set; } = 30.00m;

    // ============================================================
    // SHARED MILESTONE WEIGHTS (used in Tech and Cost, must sum to 100)
    // ============================================================

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Milestone_SRR_WT { get; set; } = 25.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Milestone_PDR_WT { get; set; } = 35.00m;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Milestone_CDR_WT { get; set; } = 40.00m;

    // ============================================================
    // METADATA
    // ============================================================

    [Required]
    public bool IsActive { get; set; } = true;

    public Guid? UpdatedBy { get; set; }

    [ForeignKey("UpdatedBy")]
    public User? UpdatedByUser { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(500)]
    public string? Comments { get; set; }
}
