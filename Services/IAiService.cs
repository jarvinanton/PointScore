using PointScore.Models.DTOs;

namespace PointScore.Services;

/// <summary>
/// Service interface for AI-powered determinations and assessments.
/// </summary>
public interface IAiService
{
    /// <summary>
    /// Automatically determines the TRL level for a WSM request using AI analysis.
    /// Gathers WSM form data, initial ERB/SIA, detailed SIA, TRL walk-through questions,
    /// and TRL definitions to produce a reasoned determination.
    /// </summary>
    Task<AiTrlDeterminationResponseDto> DetermineTrlAsync(Guid wsmRequestId);

    /// <summary>
    /// Returns AI initial ERB/SIA assessment predictions for a WSM request.
    /// Predicts per-functional-area impact with confidence scores and reasoning.
    /// </summary>
    Task<AiErbSiaAssessmentDto> GetInitialErbSiaAssessmentAsync(Guid wsmRequestId);

    /// <summary>
    /// Generates an AI-powered contextual response for the AI assistant chat.
    /// Uses WSM data as context when a wsmRequestId is provided.
    /// </summary>
    Task<AiChatResponseDto> QueryAiAssistantAsync(string query, Guid? wsmRequestId);

    /// <summary>
    /// Generates a CDRL draft from a natural language prompt.
    /// Optionally uses a WSM request as context.
    /// </summary>
    Task<AiCdrlDraftDto> GenerateCdrlDraftAsync(string prompt, Guid? wsmRequestId);
}
