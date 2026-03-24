using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models
{
    public class WsmInterfaceDetail : BaseEntityGuid
    {
        [Required]
        public Guid WsmRequestId { get; set; }

        [ForeignKey("WsmRequestId")]
        public virtual WsmRequest? WsmRequest { get; set; }

        public Guid? TargetWsmId { get; set; }

        [ForeignKey("TargetWsmId")]
        public virtual WsmRequest? TargetWsm { get; set; }


        public bool HasPhysical { get; set; }
        public bool HasEnergy { get; set; }
        public bool HasMass { get; set; }
        public bool HasInfo { get; set; }

        public bool IsInternal { get; set; } = false;

        [MaxLength(500)]
        public string? Comments { get; set; }
    }
}
