
using System.ComponentModel.DataAnnotations;

namespace PointScore.Models.DTOs
{
    public class FeatureScoreInputDto
    {
        // Propiedades de ENTRADA (mapeadas desde la clase FeatureScoreResult)
        public int? ProjectId { get; set; }
        public Guid? WsmRequestId { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal? ENG_SIA { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? ENG_SIA_WT { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal? FNTL_SIA { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? FNTL_SIA_WT { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal? COST_IMP { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? COST_WT { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal? SCH_IMP { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? SCH_IMP_WT { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal? TECH_IMP { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? TECH_WT { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal? FNTL_IMP { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? FNTL_IMP_WT { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal? TRL { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? TRL_WT { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal? DELIV { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? DELIV_WT { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal? MRL { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? MRL_WT { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal? SRR { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? SRR_WT { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal? PDR { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? PDR_WT { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal? CDR { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? CDR_WT { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? SRR_PDR_CDR_WT { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal? INTERD { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? INTERD_WT { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal? SELFDEP { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? SELFDEP_WT { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal? ORI { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? ORI_WT { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal? USER_IMP { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? USER_IMP_WT { get; set; }

        [Required]
        [Range(1, 10)]
        public decimal? TIME_CRIT { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? TIME_CRIT_WT { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? HICAT_TECH_WT { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? HICAT_FNTL_WT { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? HICAT_SCH_WT { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? HICAT_USER_WT { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal? HICAT_COST_WT { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int? DEV_COST { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int? PROD_COST { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int? PROD_QTY { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int? TOT_PROD_COST { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int? INSTALL_COST { get; set; }
        //public string AI_RESPONSE { get; set; } = string.Empty;
    }
}