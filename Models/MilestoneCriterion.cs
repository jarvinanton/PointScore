using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models
{
    public class MilestoneCriterion : BaseEntityGuid

    {
        [Required]
        [MaxLength(10)]
        public string MilestoneType { get; set; } = string.Empty; // "SRR", "PDR", "CDR"

        [Required]
        [MaxLength(200)]
        public string Product { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string FunctionalGroup { get; set; } = string.Empty;

        [Required]
        public string Criteria { get; set; } = string.Empty;

        [MaxLength(50)]
        public string RequiredOptionalTailored { get; set; } = string.Empty; // "R", "O", "T", "N/A", "Y"

        public string? IprOqe { get; set; } // Evidence Objetivo de Calidad

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Complexity { get; set; } // Complexity (1-10)

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CreationTime { get; set; } // Creation Time (weeks)

        public bool IsBlockLevel { get; set; } // If called "BLOCK" in Excel, Engineering covers it

        public bool IsExitCriteria { get; set; } // Solucion de la tarea 232 (Add attributes of SRR Entry / Exit (E/E) Criteria into database and link to E/E and WSM)

        [MaxLength(100)]
        public string? ReferenceSection { get; set; } // Reference to SETR/NAVAIR standards

        [MaxLength(100)]
        public string? ExpectedOqeLevel { get; set; } // Target maturity (e.g., "Draft", "Final")
        
        public bool IsActive { get; set; } = true;


        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<WsmMilestoneResponse> Responses { get; set; } = new List<WsmMilestoneResponse>();

    }
}
