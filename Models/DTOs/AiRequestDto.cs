

namespace PointScore.Models.DTOs;

public class UpdatedScoreDataDto
{
    // He ajustado los nombres para que coincidan con FeatureScoreResult
    public decimal ScoreMetrics { get; set; }
    public decimal WsmCompositeScore { get; set; }
    public decimal HighCategoryTechnicalScore { get; set; }
    public decimal RiskToleranceScoreLikelikhood { get; set; }
    public decimal RiskToleranceScoreConsequence { get; set; }
}

public class AiRequestDto
{
    public string? AiResponse { get; set; }
    public UpdatedScoreDataDto? UpdatedScoreData { get; set; }
}