using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models
{
    public class WsmMilestoneScore : BaseEntityGuid
    {
        [Required]
        public Guid WsmRequestId { get; set; }

        [ForeignKey("WsmRequestId")]
        public virtual WsmRequest? WsmRequest { get; set; }

        [Required]
        [MaxLength(10)]
        public string MilestoneType { get; set; } = string.Empty; // "SRR", "PDR", "CDR"

        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalPotentialScore { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalApplicableScore { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal FinalScore { get; set; } // (Applicable / Potential) * 10
    }
}
