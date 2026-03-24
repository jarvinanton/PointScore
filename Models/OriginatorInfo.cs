using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PointScore.Models;


namespace PointScore.Models;
// OriginatorInfo.cs
public class OriginatorInfo
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(200)]
    public string FullName { get; set; } = null!;

    [MaxLength(150)]
    public string? Title { get; set; }

    [MaxLength(254)]
    public string? Email { get; set; }

    [MaxLength(150)]
    public string? Organization { get; set; }

    [Phone]
    public string? ContactNumber { get; set; }

    // Navegación
    public ICollection<WsmRequest> OriginatedRequests { get; set; } = new List<WsmRequest>();

}