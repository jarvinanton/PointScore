using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PointScore.Models;

namespace PointScore.Models{

public class ImpactAnalysis
{
   [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid WsmRequestId { get; set; }

    public bool Software { get; set; }
    // public bool? SoftwareApplicability { get; set; }  // o el nombre exacto del PDF
    public bool Cyber { get; set; }
    public bool Training { get; set; }
    public bool Safety { get; set; }
    public bool Requirements { get; set; }
    public bool ProductionLine { get; set; }
    public bool DevelopmentSIL { get; set; }
    public bool FieldTacticalSIL { get; set; }
    public bool TestEvent { get; set; }
    public bool DepotRepair { get; set; }
    public bool OtherImpact { get; set; }

    public bool SoftwareApplicability { get; set; }
    public bool Test { get; set; }
    public bool Quality { get; set; }
    public bool Logistics { get; set; }
    public bool SystemsEngineering { get; set; }
    public bool AcquisitionContracts { get; set; }
    public bool Finance { get; set; }
    public bool ProgramManagement { get; set; }
    public bool Security { get; set; }

    public WsmRequest WsmRequest { get; set; } = null!;
}
}