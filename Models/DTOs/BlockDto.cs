using System;
using System.ComponentModel.DataAnnotations;
using PointScore.Models.Enums;


namespace PointScore.Models.DTOs;

// DTOs
public class CreateBlockDto
{
    [Required, MaxLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    // public Guid? BlockOwnerId { get; set; }
    public Guid BlockOwnerId { get; set; }

    /// <summary>
    /// Capacidad máxima de puntos (int * 100). Ejemplo: 250.50 puntos = 25050
    /// </summary>
    public int? MaxPointCapacity { get; set; }

    /// <summary>
    /// Costo máximo en centavos (int * 100). Ejemplo: $1,500.75 = 150075
    /// </summary>
    public int? MaxCost { get; set; }
}

public class BlockResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid? BlockOwnerId { get; set; }
    public string? BlockOwnerName { get; set; }
    public bool IsApproved { get; set; }
    
    /// <summary>
    /// Capacidad máxima de puntos (int * 100). Ejemplo: 250.50 puntos = 25050
    /// </summary>
    public int? MaxPointCapacity { get; set; }
    
    /// <summary>
    /// Costo máximo en centavos (int * 100). Ejemplo: $1,500.75 = 150075
    /// </summary>
    public int? MaxCost { get; set; }
    public decimal TotalPoints { get; set; }
    public DateTime CreatedAt { get; set; }
}

// DTO para el listado
public class BlockListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid? BlockOwnerId { get; set; }
    public string BlockOwnerName { get; set; } = null!;
    public string BlockOwnerEmail { get; set; } = null!;
    public bool IsApproved { get; set; }
    
    /// <summary>
    /// Capacidad máxima de puntos (int * 100). Ejemplo: 250.50 puntos = 25050
    /// </summary>
    public int? MaxPointCapacity { get; set; }
    
    /// <summary>
    /// Costo máximo en centavos (int * 100). Ejemplo: $1,500.75 = 150075
    /// </summary>
    public int? MaxCost { get; set; }
    public int WsmCount { get; set; }
    public decimal TotalPoints { get; set; }
}
public class BlockDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsApproved { get; set; }
    
    /// <summary>
    /// Capacidad máxima de puntos (int * 100). Ejemplo: 250.50 puntos = 25050
    /// </summary>
    public int? MaxPointCapacity { get; set; }
    
    /// <summary>
    /// Costo máximo en centavos (int * 100). Ejemplo: $1,500.75 = 150075
    /// </summary>
    public int? MaxCost { get; set; }
    public PersonDto? BlockOwner { get; set; }
}
public class UpdateBlockDto
{
    public string? Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Guid? BlockOwnerId { get; set; }

    public bool? IsApproved { get; set; }

    /// <summary>
    /// Capacidad máxima de puntos (int * 100). Ejemplo: 250.50 puntos = 25050
    /// </summary>
    public int? MaxPointCapacity { get; set; }

    /// <summary>
    /// Costo máximo en centavos (int * 100). Ejemplo: $1,500.75 = 150075
    /// </summary>
    public int? MaxCost { get; set; }
}

public class AssignWsmsToBlockDto
{
    [Required]
    public List<WsmAssignmentEntry> Wsms { get; set; } = new();
}

public class WsmAssignmentEntry
{
    [Required]
    public Guid WsmRequestId { get; set; }

    // Opcional: puedes agregar status si quieres controlarlo aquí
    public string Status { get; set; } = "Assigned"; // Recommended, Assigned, Approved
}
public class UpdateBlockOwnerDto
{
    public Guid? BlockOwnerId { get; set; }  // null = quitar owner
}