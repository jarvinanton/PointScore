using System.ComponentModel.DataAnnotations;

namespace PointScore.Models.DTOs
{
    /// <summary>
    /// DTO para crear una asignación de deliverable a un WSM
    /// </summary>
    public class CreateDeliverableAssignmentDto
    {
        [Required]
        public Guid WsmRequestId { get; set; }

        [Required]
        public int DeliverableCatalogId { get; set; }

        public DateTime? CompleteEstimateAt { get; set; }

        public Guid? ResponsibleIptMemberId { get; set; }

        [Range(1, 10)]
        public int? Priority { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }
    }

    /// <summary>
    /// DTO para un deliverable individual en una asignación masiva
    /// </summary>
    public class BulkDeliverableItemDto
    {
        [Required]
        public int DeliverableCatalogId { get; set; }

        public DateTime? CompleteEstimateAt { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        [Range(1, 10)]
        public int? Priority { get; set; }
    }

    /// <summary>
    /// DTO para asignar múltiples deliverables a un WSM
    /// </summary>
    public class BulkAssignDeliverablesDto
    {
        [Required]
        public Guid WsmRequestId { get; set; }

        [Required]
        [MinLength(1)]
        public List<BulkDeliverableItemDto> Deliverables { get; set; } = new();

        public Guid? ResponsibleIptMemberId { get; set; }

        public DateTime? DefaultCompleteEstimateAt { get; set; }

        [MaxLength(1000)]
        public string? DefaultNotes { get; set; }
    }

    /// <summary>
    /// DTO para actualizar una asignación existente
    /// </summary>
    public class UpdateDeliverableAssignmentDto
    {
        public DateTime? CompleteEstimateAt { get; set; }

        public DateTime? DeliveredAt { get; set; }

        [MaxLength(50)]
        public string? Status { get; set; }

        public Guid? ResponsibleIptMemberId { get; set; }

        [Range(0, 100)]
        public int? CompletionPercentage { get; set; }

        [Range(1, 10)]
        public int? Priority { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }
    }

    /// <summary>
    /// DTO para respuesta de asignación con información del deliverable del catálogo
    /// </summary>
    public class DeliverableAssignmentResponseDto
    {
        public Guid Id { get; set; }
        public Guid WsmRequestId { get; set; }
        public string WsmRequestNumber { get; set; } = null!;
        public int DeliverableCatalogId { get; set; }
        public string DeliverableDIDNumber { get; set; } = null!;
        
        // Información del deliverable del catálogo
        public string? DeliverableDescription { get; set; }
        public string? DeliverableType { get; set; }
        public string? FunctionalArea { get; set; }
        public decimal? Complexity { get; set; }
        public decimal? TotalScore { get; set; }
        public decimal? ApplicableWSMScore { get; set; }

        // Información de la asignación
        public DateTime AssignedAt { get; set; }
        public DateTime? CompleteEstimateAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public string Status { get; set; } = null!;
        public int? CompletionPercentage { get; set; }
        public int? Priority { get; set; }
        public string? Notes { get; set; }

        // Información del responsable
        public Guid? ResponsibleIptMemberId { get; set; }
        public string? ResponsibleIptMemberName { get; set; }
        public string? ResponsibleIptMemberEmail { get; set; }

        // Metadatos
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedByUserName { get; set; }

        // Indicadores calculados
        public bool IsOverdue { get; set; }
        public int? DaysUntilDue { get; set; }
    }

    /// <summary>
    /// DTO para filtrar asignaciones de deliverables
    /// </summary>
    public class DeliverableAssignmentFilterDto
    {
        public Guid? WsmRequestId { get; set; }
        public int? DeliverableCatalogId { get; set; }
        public string? Status { get; set; }
        public Guid? ResponsibleIptMemberId { get; set; }
        public string? FunctionalArea { get; set; }
        public string? DeliverableType { get; set; } // CDRL, NON-CDRL
        public bool? IsOverdue { get; set; }
        public DateTime? AssignedAfter { get; set; }
        public DateTime? AssignedBefore { get; set; }
        public DateTime? DueBefore { get; set; }
        public int? MinPriority { get; set; }
        public int? MaxPriority { get; set; }
        public int? MinCompletionPercentage { get; set; }
        public int? MaxCompletionPercentage { get; set; }
    }

    /// <summary>
    /// DTO para estadísticas de asignaciones
    /// </summary>
    public class DeliverableAssignmentStatsDto
    {
        public int TotalAssignments { get; set; }
        public int AssignedCount { get; set; }
        public int InProgressCount { get; set; }
        public int CompletedCount { get; set; }
        public int DeliveredCount { get; set; }
        public int CancelledCount { get; set; }
        public int OverdueCount { get; set; }
        public decimal AverageCompletionPercentage { get; set; }
        public Dictionary<string, int> ByFunctionalArea { get; set; } = new();
        public Dictionary<string, int> ByStatus { get; set; } = new();
    }

    /// <summary>
    /// DTO para actualizar solo el estado
    /// </summary>
    public class UpdateStatusDto
    {
        [Required]
        public string Status { get; set; } = null!;
    }
}
