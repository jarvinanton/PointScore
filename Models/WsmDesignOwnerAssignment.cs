using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PointScore.Models.Enums;

namespace PointScore.Models
{
    public class WsmDesignOwnerAssignment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid WsmRequestId { get; set; }

        [ForeignKey("WsmRequestId")]
        public virtual WsmRequest WsmRequest { get; set; } = null!;

        [Required]
        public Guid DesignOwnerId { get; set; }

        [ForeignKey("DesignOwnerId")]
        public virtual RecommendedDesignOwner DesignOwner { get; set; } = null!;

        [Required]
        public RecommendedDesignOwnerStatus Status { get; set; } = RecommendedDesignOwnerStatus.Recommended;

        public DateTime AssignmentDate { get; set; } = DateTime.UtcNow;

        public DateTime? StatusChangedDate { get; set; }

        public Guid? AssignedByUserId { get; set; }

        [ForeignKey("AssignedByUserId")]
        public virtual User? AssignedByUser { get; set; }

        [MaxLength(1000)]
        public string? StatusComments { get; set; }
    }
}
