using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models;

public class Deliverable
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(100)]
    public string Code { get; set; } = null!;           // Ej: CDRL-001, NON-CDRL-101

    [Required, MaxLength(200)]
    public string Title { get; set; } = null!;          // Ej: "Test Report", "Software Development Plan"

    [MaxLength(500)]
    public string? Description { get; set; }

    public bool IsCdrl { get; set; } = true;            // true = CDRL, false = Non-CDRL

    public bool IsActive { get; set; } = true;          // Para desactivar sin borrar

    // Relación muchos-a-muchos con WSM
    public ICollection<WsmDeliverable> WsmDeliverables { get; set; } = new List<WsmDeliverable>();
}

public class WsmDeliverable
{
    public Guid WsmRequestId { get; set; }
    public Guid DeliverableId { get; set; }

    public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
    public DateTime? DeliveredDate { get; set; }
    public string? Status { get; set; } = "Assigned"; // Assigned, In Progress, Delivered, Accepted

    public WsmRequest WsmRequest { get; set; } = null!;
    public Deliverable Deliverable { get; set; } = null!;
}