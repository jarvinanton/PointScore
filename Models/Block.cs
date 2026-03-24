using System.ComponentModel.DataAnnotations;

namespace PointScore.Models;

public class Block
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(50)]
    public string Name { get; set; } = null!;  // BLOCK-001

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    // Block Owner – persona del proyecto
    public Guid? BlockOwnerId { get; set; }
    public User? BlockOwner { get; set; }

    public bool IsApproved { get; set; } = false;

    // Capacidad máxima de puntos (int * 100)
    // Ejemplo: 250.50 puntos = 25050
    public int? MaxPointCapacity { get; set; }

    // Costo máximo en centavos (int * 100)
    // Ejemplo: $1,500.75 = 150075
    public int? MaxCost { get; set; }

    // NAVEGACIÓN: WSMs que pertenecen a este Block
    public ICollection<WsmRequest> Wsms { get; set; } = new List<WsmRequest>();
}