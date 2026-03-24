using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models
{
    public class WsmTimeCriticalityAssessment : BaseEntityGuid
    {
        [Required]
        public Guid WsmRequestId { get; set; }

        [ForeignKey("WsmRequestId")]
        public virtual WsmRequest? WsmRequest { get; set; }

        // Inputs (1-7)
        // Rows: 1=ASAP, 2=<1m, 3=1-3m, 4=3-6m, 5=6-9m, 6=9-12m, 7=12m+
        [Range(1, 7)]
        public int BeginWorkNeedTime { get; set; }

        // Columns: 1=<1m, 2=<3m, 3=<6m, 4=<12m, 5=<18m, 6=<24m, 7=>24m
        [Range(1, 7)]
        public int CompleteWorkNeedTime { get; set; }

        // Calculated Score (0, 2.5, 5, 7.5, 10)
        public double Score { get; set; }
    }
}
