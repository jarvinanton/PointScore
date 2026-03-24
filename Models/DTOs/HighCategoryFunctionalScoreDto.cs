using System;

namespace PointScore.Models.DTOs
{
    public class HighCategoryFunctionalScoreDto
    {
        public decimal FNTL_SIA_Score { get; set; }
        public decimal FNTL_IMP_Score { get; set; }

        public decimal TotalHighCategoryFunctionalScore { get; set; }
    }
}
