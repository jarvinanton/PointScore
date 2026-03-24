using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models
{
    public class MRLResponse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid WsmRequestId { get; set; }

        [ForeignKey("WsmRequestId")]
        public virtual WsmRequest WsmRequest { get; set; } = null!;

        [Required]
        public int MRLSubThreadId { get; set; }

        [ForeignKey("MRLSubThreadId")]
        public virtual MRLSubThread MRLSubThread { get; set; } = null!;

        [Range(1, 10)]
        public int Score { get; set; }

        public DateTime LastModified { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        [ForeignKey("UpdatedByUserId")]
        public virtual User? UpdatedByUser { get; set; }
    }
}
