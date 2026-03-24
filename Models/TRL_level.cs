using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models
{
    [Table("TRL_levels")]
    public class TRL_level
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid WsmRequestId { get; set; }
        
        [ForeignKey("WsmRequestId")]
        public virtual WsmRequest WsmRequest { get; set; } = null!;

        public decimal Score { get; set; }

        public int Level { get; set; }

        public string? Definition { get; set; }
    }
}
