using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models;

public class WsmFunctionalImpact
{
    // Clave compuesta (Many-to-Many entre WsmRequest y FunctionalArea)
    public Guid WsmRequestId { get; set; }
    public int FunctionalAreaId { get; set; }

    // Initial SIA – ¿Está impactada esta área?
    public bool IsImpacted { get; set; } = true;

    // ERB Review – Revisor asignado para esta área funcional
    public Guid? ReviewerId { get; set; }  // FK a WsmOwner (usuario del proyecto)
    public User? Reviewer { get; set; }

    // Estado de revisión por el revisor
    public string Status { get; set; } = "Assigned"; // Assigned, InReview, Reviewed, Approved, Rejected

    public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ReviewedDate { get; set; }

    [MaxLength(1000)]
    public string? Comments { get; set; }

    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    // Navegación
    public WsmRequest WsmRequest { get; set; } = null!;
    public FunctionalArea FunctionalArea { get; set; } = null!;
}