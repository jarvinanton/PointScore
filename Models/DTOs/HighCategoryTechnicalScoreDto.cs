using System;

namespace PointScore.Models.DTOs
{
    public class HighCategoryTechnicalScoreDto
    {
        public decimal ENG_SIA_Score { get; set; }
        public decimal TECH_IMP_Score { get; set; }
        public decimal TRL_Score { get; set; }
        public decimal DELIV_Score { get; set; }
        public decimal SRR_Score { get; set; }
        public decimal PDR_Score { get; set; }
        public decimal CDR_Score { get; set; }
        public decimal INTERD_Score { get; set; }
        public decimal SELFDEP_Score { get; set; }

        public decimal TotalHighCategoryTechnicalScore { get; set; }
    }
}
