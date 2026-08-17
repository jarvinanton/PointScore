using System;

namespace PointScore.Models.DTOs
{
    public class HighCategoryUserScoreDto
    {
        public decimal OperationalReadinessImpact_Score { get; set; }
        public decimal UserMissionImpact_Score { get; set; }

        /// <summary>
        /// Time Criticality Score (TIME_CRIT).
        /// Belongs to User per client formula.
        /// </summary>
        public decimal TimeCriticality_Score { get; set; }

        public decimal TotalHighCategoryUserScore { get; set; }
    }
}
