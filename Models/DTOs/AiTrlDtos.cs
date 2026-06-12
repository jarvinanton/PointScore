namespace PointScore.Models.DTOs;

// ==================== TRL AI Determination DTOs ====================

/// <summary>
/// TRL walk-through question used by the AI engine to assess readiness level.
/// </summary>
public class TrlWalkThroughQuestionDto
{
    public int Level { get; set; }
    public string Question { get; set; } = null!;
    public string? Context { get; set; }
}

/// <summary>
/// Complete response from the AI TRL determination service.
/// </summary>
public class AiTrlDeterminationResponseDto
{
    public Guid WsmRequestId { get; set; }
    public string RequestNumber { get; set; } = null!;
    public string SystemName { get; set; } = null!;

    /// <summary>AI-determined TRL level (1-9).</summary>
    public int DeterminedLevel { get; set; }

    /// <summary>Normalized score (0-10) mapped from the TRL level.</summary>
    public decimal DeterminedScore { get; set; }

    /// <summary>TRL definition corresponding to the determined level.</summary>
    public string TrlDefinition { get; set; } = null!;

    /// <summary>Detailed AI reasoning explaining the determination.</summary>
    public string Rationale { get; set; } = null!;

    /// <summary>Confidence level of the AI determination (0.0 – 1.0).</summary>
    public double Confidence { get; set; }

    /// <summary>Walk-through questions the AI evaluated to reach the determination.</summary>
    public List<TrlEvaluatedQuestionDto> EvaluatedQuestions { get; set; } = new();

    /// <summary>Key data points from the WSM form that drove the determination.</summary>
    public List<string> SupportingEvidence { get; set; } = new();

    /// <summary>Indicates whether this is a mocked/simulated response (no OpenAI key).</summary>
    public bool IsMocked { get; set; }

    /// <summary>Model version or identifier used.</summary>
    public string ModelVersion { get; set; } = null!;

    /// <summary>Timestamp when the determination was generated.</summary>
    public DateTime GeneratedAt { get; set; }
}

/// <summary>
/// An individual TRL walk-through question evaluated by the AI.
/// </summary>
public class TrlEvaluatedQuestionDto
{
    public int Level { get; set; }
    public string Question { get; set; } = null!;
    public bool? MeetsRequirement { get; set; }
    public string? AiComment { get; set; }
}

// ==================== ERB/SIA Assessment DTOs ====================

/// <summary>
/// Per-functional-area AI prediction for initial ERB/SIA assessment.
/// </summary>
public class AiFunctionalAreaPredictionDto
{
    public int FunctionalAreaId { get; set; }
    public string FunctionalAreaName { get; set; } = null!;
    public bool IsImpacted { get; set; }
    public double Confidence { get; set; }
    public string Reasoning { get; set; } = null!;
}

/// <summary>
/// Full AI initial ERB/SIA assessment for a WSM request.
/// </summary>
public class AiErbSiaAssessmentDto
{
    public Guid WsmRequestId { get; set; }
    public string RequestNumber { get; set; } = null!;
    public string SystemName { get; set; } = null!;
    public string Title { get; set; } = null!;
    public List<AiFunctionalAreaPredictionDto> FunctionalAreaPredictions { get; set; } = new();
    public string SummaryNarrative { get; set; } = null!;
    public bool IsMocked { get; set; }
    public string ModelVersion { get; set; } = null!;
    public DateTime GeneratedAt { get; set; }
}

// ==================== AI Chat DTOs ====================

/// <summary>Response from the AI assistant chat endpoint.</summary>
public class AiChatResponseDto
{
    public string Response { get; set; } = null!;
    public bool IsMocked { get; set; }
}

/// <summary>Request payload for the AI chat endpoint.</summary>
public class AiChatQueryDto
{
    public string Query { get; set; } = null!;
    public Guid? WsmRequestId { get; set; }
}

// ==================== AI CDRL Generator DTOs ====================

/// <summary>Request payload for CDRL draft generation.</summary>
public class AiCdrlGenerateRequestDto
{
    public string Prompt { get; set; } = null!;
    public Guid? WsmRequestId { get; set; }
}

/// <summary>AI-generated CDRL draft.</summary>
public class AiCdrlDraftDto
{
    public string Explanation { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string FunctionalArea { get; set; } = null!;
    public string WhenNeeded { get; set; } = null!;
    public decimal EstimatedCost { get; set; }
    public bool IsMocked { get; set; }
}
