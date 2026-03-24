using System.ComponentModel.DataAnnotations;

namespace PointScore.Models.DTOs;

public class SiaScoreInputDto
{
    [Required] public decimal Logistics_Score { get; set; }
    [Required] public decimal Logistics_Weight { get; set; }

    [Required] public decimal ProductionGFE_Score { get; set; }
    [Required] public decimal ProductionGFE_Weight { get; set; }

    [Required] public decimal Safety_Score { get; set; }
    [Required] public decimal Safety_Weight { get; set; }

    [Required] public decimal Quality_Score { get; set; }
    [Required] public decimal Quality_Weight { get; set; }

    [Required] public decimal Cyber_Score { get; set; }
    [Required] public decimal Cyber_Weight { get; set; }

    [Required] public decimal Software_Score { get; set; }
    [Required] public decimal Software_Weight { get; set; }

    [Required] public decimal SystemsEngineering_Score { get; set; }
    [Required] public decimal SystemsEngineering_Weight { get; set; }

    [Required] public decimal Test_Score { get; set; }
    [Required] public decimal Test_Weight { get; set; }

    [Required] public decimal AcquisitionContracts_Score { get; set; }
    [Required] public decimal AcquisitionContracts_Weight { get; set; }

    [Required] public decimal Finance_Score { get; set; }
    [Required] public decimal Finance_Weight { get; set; }

    [Required] public decimal ProgramManagement_Score { get; set; }
    [Required] public decimal ProgramManagement_Weight { get; set; }

    [Required] public decimal Security_Score { get; set; }
    [Required] public decimal Security_Weight { get; set; }
}

public class SiaScoreOutputDto
{
    public decimal EA_Score { get; set; }
    public decimal OFA_Score { get; set; }
    public decimal NE_Score { get; set; }
    public decimal NOFA_Score { get; set; }
}

public class SiaScoreDetailDto : SiaScoreInputDto
{
    public int Id { get; set; }
    public Guid WsmRequestId { get; set; }
    
    // Outputs included flat
    public decimal EA_Score { get; set; }
    public decimal OFA_Score { get; set; }
    public decimal NE_Score { get; set; }
    public decimal NOFA_Score { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}
