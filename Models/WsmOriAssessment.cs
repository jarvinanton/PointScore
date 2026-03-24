using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models
{
    public class WsmOriAssessment : BaseEntityGuid
    {
        [Required]
        public Guid WsmRequestId { get; set; }

        [ForeignKey("WsmRequestId")]
        public virtual WsmRequest? WsmRequest { get; set; }

        // Inputs (1-5)
        [Range(1, 5)]
        public int Likelihood { get; set; }

        [Range(1, 5)]
        public int Consequence { get; set; }

        // Calculated Score (0, 3.3, 5.0, 6.7, 10.0)
        public double Score { get; set; }

    }
}
