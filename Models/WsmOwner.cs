using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PointScore.Models;

namespace PointScore.Models
{
    public class WsmOwner
    {
       [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(200)]
    public string FullName { get; set; } = null!;

    [MaxLength(254)]
    public string? Email { get; set; }

    [MaxLength(150)]
    public string? Organization { get; set; }
    public Guid? UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User? User { get; set; }


    // Navegación
	public ICollection<WsmRequest> OwnedWsmRequests { get; set; } = new List<WsmRequest>();
	// public ICollection<WsmRequest> BlockOwnedRequests { get; set; } = new List<WsmRequest>();
    }
}
