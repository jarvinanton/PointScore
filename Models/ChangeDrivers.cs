using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PointScore.Models;

namespace PointScore.Models;

public class ChangeDriverType
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string Name { get; set; } = null!; // Warfighter, ECP, Obsolescence, etc.
}

public class WsmChangeDriver
{
    public Guid WsmRequestId { get; set; }
    public int ChangeDriverTypeId { get; set; }

    [Key, Column(Order = 0)] public Guid WsmRequestIdKey => WsmRequestId;
    [Key, Column(Order = 1)] public int ChangeDriverTypeIdKey => ChangeDriverTypeId;

    public WsmRequest WsmRequest { get; set; } = null!;
    public ChangeDriverType ChangeDriverType { get; set; } = null!;
}

public class ModificationType
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string Name { get; set; } = null!;
}

public class WsmOtherDetail
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WsmRequestId { get; set; }
    public WsmRequest WsmRequest { get; set; } = null!;

    public string Type { get; set; } = null!; // "Category"
    public string Description { get; set; } = null!; // Texto que escribe el usuario

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}