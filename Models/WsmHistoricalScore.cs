using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models
{
    [Table("WsmHistoricalScores")]
    public class WsmHistoricalScore : BaseEntityGuid
    {
        [Required]
        public Guid WsmRequestId { get; set; }

        [ForeignKey("WsmRequestId")]
        public virtual WsmRequest? WsmRequest { get; set; }

        [Required]
        [MaxLength(50)]
        public string RequestNumber { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string SystemName { get; set; } = null!;

        // --- Computed Scores ---
        [Column(TypeName = "decimal(18,4)")]
        public decimal TechnicalScore { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal FunctionalScore { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal UserScore { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal ScheduleScore { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal CostScore { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal WsmCompositeScore { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal CostCorrelatedScore { get; set; }

        // --- Metric Scores ---
        [Column(TypeName = "decimal(18,4)")]
        public decimal TrlScore { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal MrlScore { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal OriScore { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal TimeCriticalityScore { get; set; }

        // --- SIA Scores ---
        [Column(TypeName = "decimal(18,4)")]
        public decimal SiaEAScore { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal SiaOFAScore { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal SiaNEScore { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal SiaNOFAScore { get; set; }

        // --- Costs ---
        [Column(TypeName = "decimal(18,2)")]
        public decimal DevelopmentCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalProductionCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal InstallationCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalWsmCost { get; set; }

        // --- Metadata for audit ---
        [Required]
        [MaxLength(100)]
        public string CapturedBy { get; set; } = "System";

        [Required]
        [MaxLength(50)]
        public string Version { get; set; } = "1.0";
    }
}
