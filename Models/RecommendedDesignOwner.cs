using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PointScore.Models;
using PointScore.Models.Enums;


// 1. First, let's create the RecommendedDesignOwner model
public class RecommendedDesignOwner
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [MaxLength(200)]
    public string OrganizationName { get; set; } = null!;
    
    [Required]
    [MaxLength(100)]
    public string ContactPerson { get; set; } = null!;
    
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string ContactEmail { get; set; } = null!;
    
    public DateTime AssignmentDate { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Estado de la recomendación del Design Owner
    /// Valores posibles: Recommended (0), Approved (1), Denied (2)
    /// </summary>
    [Required]
    public RecommendedDesignOwnerStatus Status { get; set; } = RecommendedDesignOwnerStatus.Recommended;
    
    /// <summary>
    /// Fecha en que se cambió el estado (aprobación o denegación)
    /// </summary>
    public DateTime? StatusChangedDate { get; set; }
    
    /// <summary>
    /// Comentarios sobre la aprobación o denegación
    /// </summary>
    [MaxLength(1000)]
    public string? StatusComments { get; set; }
    
    // Navigation property
    public virtual ICollection<WsmRequest> WsmRequests { get; set; } = new List<WsmRequest>();
}
