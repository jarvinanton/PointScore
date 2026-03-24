using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models
{
    public class WsmMilestoneResponse : BaseEntityGuid
    {
        [Required]
        public Guid WsmRequestId { get; set; }

        [ForeignKey("WsmRequestId")]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual WsmRequest WsmRequest { get; set; } = null!;

        [Required]
        public Guid MilestoneCriterionId { get; set; }

        [ForeignKey("MilestoneCriterionId")]
        public virtual MilestoneCriterion MilestoneCriterion { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Response { get; set; } = string.Empty; // "Y", "N", "T", "O", "N/A", "R"

        public string? OqeEvidence { get; set; } // Link or description of the evidence

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalScore { get; set; } // Calculated based on complexity and time

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ApplicableScore { get; set; } // Calculated based on TotalScore and Selection

        public Guid? UpdatedByUserId { get; set; }


        [ForeignKey("UpdatedByUserId")]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual User? UpdatedByUser { get; set; }

    }
}
