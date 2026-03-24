using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PointScore.Models;

namespace PointScore.Models; 

public class WsmWbsStructure
{
   [Key]
    public int Id { get; set; }

    public Guid WsmRequestId { get; set; }
    public string Level { get; set; } = null!; // Level1, Level2, etc.
    public string Nomenclature { get; set; } = null!;
    public bool Applicability { get; set; }

    public WsmRequest WsmRequest { get; set; } = null!;
}
