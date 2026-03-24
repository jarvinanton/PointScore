using System.ComponentModel.DataAnnotations;

namespace PointScore.Models.DTOs
{
    public class TrlLevelDto
    {
        [Range(0, 999999.99)]
        public decimal Score { get; set; }
        
        public int Level { get; set; }
        
        public string? Definition { get; set; }

        public Guid? WsmRequestId { get; set; }
    }
}
