using System;

namespace PointScore.Models.DTOs
{
    public class HighCategoryUserScoreDto
    {
        public decimal OperationalReadinessImpact_Score { get; set; }
        public decimal UserMissionImpact_Score { get; set; }

        public decimal TotalHighCategoryUserScore { get; set; }
    }
}
