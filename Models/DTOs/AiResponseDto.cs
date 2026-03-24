

namespace PointScore.Models.DTOs;

public class AiResponseDto
{
    public string? AiRequest { get; set; }
    public FeatureScoreResult? ScoreData { get; set; }
}