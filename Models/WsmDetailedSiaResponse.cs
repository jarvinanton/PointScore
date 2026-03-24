using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models;
public class WsmDetailedSiaResponse

{
    public Guid WsmRequestId { get; set; }
    public int DetailedSiaSectionId { get; set; }

    public bool IsImpacted { get; set; } = false;  // Yes/No (false = no impactado en esta sección)
    public string? Comments { get; set; }

    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    // public string? UpdatedById { get; set; }  // User.Id que respondió

    public WsmRequest WsmRequest { get; set; } = null!;
    public DetailedSiaSection Section { get; set; } = null!;
}