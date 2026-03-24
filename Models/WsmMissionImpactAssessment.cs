using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models
{
    public class WsmMissionImpactAssessment : BaseEntityGuid
    {
        [Required]
        public Guid WsmRequestId { get; set; }

        [ForeignKey("WsmRequestId")]
        public virtual WsmRequest? WsmRequest { get; set; }

        /// <summary>
        /// REQUIREMENT LEVEL (Rows)
        /// 1 = Good Idea
        /// 2 = Desired
        /// 3 = R-DOC Reqt
        /// 4 = Warfighter Directed
        /// 5 = JEON JUON
        /// </summary>
        [Range(1, 5)]
        public int RequirementLevel { get; set; }

        /// <summary>
        /// BENEFIT TO MISSION (Columns)
        /// Range: 1-5
        /// </summary>
        [Range(1, 5)]
        public int BenefitToMission { get; set; }

        /// <summary>
        /// Calculated Score based on the 5x5 matrix
        /// High: 10
        /// Med-High: 7.5
        /// Medium: 5
        /// Low-Med: 2.5
        /// Low: 0
        /// </summary>
        public double Score { get; set; }
    }
}
