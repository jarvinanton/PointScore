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
    /// System Impact Assessment - Functional Level Impact (FNTL_IMP).
    /// Score from detailed SIA responses for functional areas.
    /// </summary>
    public decimal FntL_Imp_Score { get; set; }

    /// <summary>
    /// Final High Category Schedule Score after applying weights and the category weight (20%).
    /// </summary>
    public decimal TotalHighCategoryScheduleScore { get; set; }
}
