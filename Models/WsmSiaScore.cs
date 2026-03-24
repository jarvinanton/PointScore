using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models;

[Table("WsmSiaScores")]
public class WsmSiaScore
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid WsmRequestId { get; set; }

    [ForeignKey("WsmRequestId")]
    public WsmRequest WsmRequest { get; set; } = null!;

    // === FUNCTIONAL AREA INPUTS (Score & Weight) ===

    // 1. Logistics
    public decimal Logistics_Score { get; set; }
    public decimal Logistics_Weight { get; set; }

    // 2. Production / GFE
    public decimal ProductionGFE_Score { get; set; }
    public decimal ProductionGFE_Weight { get; set; }

    // 3. Safety (Engineering)
    public decimal Safety_Score { get; set; }
    public decimal Safety_Weight { get; set; }

    // 4. Quality (Engineering)
    public decimal Quality_Score { get; set; }
    public decimal Quality_Weight { get; set; }

    // 5. Cyber (Engineering)
    public decimal Cyber_Score { get; set; }
    public decimal Cyber_Weight { get; set; }

    // 6. Software (Engineering)
    public decimal Software_Score { get; set; }
    public decimal Software_Weight { get; set; }

    // 7. Systems Engineering (Engineering)
    public decimal SystemsEngineering_Score { get; set; }
    public decimal SystemsEngineering_Weight { get; set; }

    // 8. Test (Engineering)
    public decimal Test_Score { get; set; }
    public decimal Test_Weight { get; set; }

    // 9. Acquisition (Contracts)
    public decimal AcquisitionContracts_Score { get; set; }
    public decimal AcquisitionContracts_Weight { get; set; }

    // 10. Finance
    public decimal Finance_Score { get; set; }
    public decimal Finance_Weight { get; set; }

    // 11. Program Management
    public decimal ProgramManagement_Score { get; set; }
    public decimal ProgramManagement_Weight { get; set; }

    // 12. Security
    public decimal Security_Score { get; set; }
    public decimal Security_Weight { get; set; }


    // === CALCULATED RESULTS ===

    public decimal EA_Score { get; set; }
    public decimal OFA_Score { get; set; }
    public decimal NE_Score { get; set; }
    public decimal NOFA_Score { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
