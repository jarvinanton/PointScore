using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models
{
    /// <summary>
    /// Relación entre un WSM Request y los Deliverables del catálogo
    /// Almacena el seguimiento de asignaciones de deliverables a WSMs
    /// </summary>
    [Table("WsmDeliverableAssignments")]
    public class WsmDeliverableAssignment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// ID del WSM Request
        /// </summary>
        [Required]
        public Guid WsmRequestId { get; set; }

        /// <summary>
        /// ID del deliverable del catálogo
        /// </summary>
        [Required]
        public int DeliverableCatalogId { get; set; }

        /// <summary>
        /// Fecha de asignación del deliverable al WSM
        /// </summary>
        [Required]
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Fecha estimada de completado
        /// </summary>
        public DateTime? CompleteEstimateAt { get; set; }

        /// <summary>
        /// Fecha real de entrega
        /// </summary>
        public DateTime? DeliveredAt { get; set; }

        /// <summary>
        /// Estado del deliverable: Assigned, InProgress, Completed, Delivered, Cancelled
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Assigned";

        /// <summary>
        /// ID del miembro del IPT responsable
        /// </summary>
        public Guid? ResponsibleIptMemberId { get; set; }

        /// <summary>
        /// Notas adicionales sobre la asignación
        /// </summary>
        [MaxLength(1000)]
        public string? Notes { get; set; }

        /// <summary>
        /// Porcentaje de completado (0-100)
        /// </summary>
        [Range(0, 100)]
        public int? CompletionPercentage { get; set; } = 0;

        /// <summary>
        /// Prioridad de este deliverable (1-10)
        /// </summary>
        [Range(1, 10)]
        public int? Priority { get; set; }

        /// <summary>
        /// Fecha de última actualización
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Usuario que realizó la última actualización
        /// </summary>
        public Guid? UpdatedByUserId { get; set; }

        // Navegación
        public WsmRequest WsmRequest { get; set; } = null!;
        public User? ResponsibleIptMember { get; set; }
        public User? UpdatedByUser { get; set; }
    }
}
