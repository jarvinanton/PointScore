using System;

namespace PointScore.Models.DTOs;

public class HighCategoryScheduleScoreDto
{
    /// <summary>
    /// Calculated System Impact Assessment - Tech Detail ECP (SCH_IMP).
    /// Passed as a parameter and divided by 10 per user requirement.
    /// </summary>
    public decimal SiaTechDetailEcp_Score { get; set; }

    /// <summary>
    /// Manufacturing Readiness Level (MRL) Score.
    /// Calculated using the formula: (280 - totalMrlScore) / 280 * 10.
    /// 280 is a constant representing the total possible MRL sub-thread score.
    /// </summary>
    public decimal ManufacturingReadinessLevel_Score { get; set; }

    /// <summary>
    /// Time Criticality Score.
    /// Retrieved from the WsmTimeCriticalityAssessment model.
    /// </summary>
    public decimal TimeCriticality_Score { get; set; }

    /// <summary>
    /// Final High Category Schedule Score after applying weights and the category weight (20%).
    /// </summary>
    public decimal TotalHighCategoryScheduleScore { get; set; }
}
