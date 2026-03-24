using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models
{
    public class MilestoneWeight : BaseEntityGuid
    {
        [Required]
        [MaxLength(10)]
        public string MilestoneType { get; set; } = string.Empty; // "SRR", "PDR", "CDR"

        [Column(TypeName = "decimal(5, 2)")]
        public decimal MinWeight { get; set; } // e.g. 0.40 (40%)

        [Column(TypeName = "decimal(5, 2)")]
        public decimal MaxWeight { get; set; } // e.g. 0.60 (60%)


        public bool IsActive { get; set; } = true;
    }
}
