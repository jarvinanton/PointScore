using System;

namespace PointScore.Models.DTOs;

public class HighCategoryCostScoreDto
{
    public decimal SiaTechDetailEcp_Score { get; set; }
    public decimal TRL_Score { get; set; }
    public decimal Deliverables_Score { get; set; }
    public decimal ManufacturingReadinessLevel_Score { get; set; }
    public decimal SrrPdrCdr_Score { get; set; }
    public decimal InterdependencyInterop_Score { get; set; }
    public decimal InterdependencySelf_Score { get; set; }
    public decimal TimeCriticality_Score { get; set; }
    
    /// <summary>
    /// System Impact Assessment - TECH_IMP score from detailed SIA responses.
    /// </summary>
    public decimal Tech_Impact_Score { get; set; }

    /// <summary>
    /// Calculated System Impact Assessment - COST Detail ECP.
    /// Formula: Actual Cost Score / Max Weights Total * 10
    /// </summary>
    public decimal SiaCostDetailEcp_Score { get; set; }

    public decimal TotalHighCategoryCostScore { get; set; }
}
