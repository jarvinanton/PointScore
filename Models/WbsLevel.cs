using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PointScore.Models;

namespace PointScore.Models;

public class WbsLevel
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(20)]
    public string Level { get; set; } = null!; // Level1, Level2, ...

    [Required, MaxLength(200)]
    public string Nomenclature { get; set; } = null!;
    
}