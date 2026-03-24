using System.ComponentModel.DataAnnotations;
using PointScore.Models.Enums;


namespace PointScore.Models.DTOs
{
    public class CreateDeliverableDto
    {
        [Required, MaxLength(50)] public string Code { get; set; } = null!;
        [Required, MaxLength(200)] public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsCdrl { get; set; } = true;
    }
    public class AssignDeliverablesDto
    {
        [Required] public List<Guid> DeliverableIds { get; set; } = new();
    }
}