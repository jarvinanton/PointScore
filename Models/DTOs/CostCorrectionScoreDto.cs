using System;

namespace PointScore.Models.DTOs;

public class CostCorrectionScoreDto
{
    public Guid WsmRequestId { get; set; }

    // Category Scores (HICAT)
    public decimal TechnicalScore { get; set; }
    public decimal FunctionalScore { get; set; }
    public decimal UserScore { get; set; }
    public decimal ScheduleScore { get; set; }
    public decimal CostScore { get; set; }

    // Aggregate
    public decimal WsmCompositeScore { get; set; } // Sum of Tech + Func + Sch

    // Costs
    public decimal TotalWsmCost { get; set; }

    // Final result
    public decimal CostCorrelatedScore { get; set; }
}
