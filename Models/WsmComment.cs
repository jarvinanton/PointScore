using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PointScore.Models;

namespace PointScore.Models;

public class WsmComment
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WsmRequestId { get; set; }
    public Guid AuthorId { get; set; }

    [Required]
    public string Comment { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public WsmRequest WsmRequest { get; set; } = null!;
    public WsmOwner Author { get; set; } = null!;
    
}