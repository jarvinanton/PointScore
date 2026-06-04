using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointScore.Models.DTOs;
using PointScore.Services;

/// <summary>
/// Controller for AI-powered assessments including TRL determination,
/// initial ERB/SIA predictions, and AI chat assistant.
/// Task 481: AI model/service for automatic TRL determination.
/// Task 482: Integration of the AI service call for automatic TRL determination.
/// </summary>
[ApiController]
[Route("api/ai-assessment")]
[Authorize]
public class AiAssessmentController : ControllerBase
{
    private readonly IAiService _aiService;
    private readonly ILogger<AiAssessmentController> _logger;

    public AiAssessmentController(IAiService aiService, ILogger<AiAssessmentController> logger)
    {
        _aiService = aiService;
        _logger = logger;
    }

    // ─────────────────────────────────────────────────────────────────────────
    // Task 481 / 482 — Automatic TRL Determination
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// [Task 481 / 482] Automatically determines the TRL level for a WSM request
    /// using AI analysis of WSM form data, initial ERB/SIA assessments, detailed SIA,
    /// description of work, and TRL definitions with walk-through questions.
    /// </summary>
    /// <param name="wsmRequestId">The GUID of the WSM request.</param>
    /// <returns>AI-generated TRL determination with rationale, confidence, and evaluated questions.</returns>
    [HttpGet("wsm/{wsmRequestId:guid}/determine-trl")]
    public async Task<IActionResult> DetermineTrl(Guid wsmRequestId)
    {
        try
        {
            _logger.LogInformation("TRL determination requested for WSM ID: {WsmId}", wsmRequestId);
            var result = await _aiService.DetermineTrlAsync(wsmRequestId);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("WSM not found for TRL determination: {WsmId} — {Message}", wsmRequestId, ex.Message);
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to determine TRL for WSM {WsmId}.", wsmRequestId);
            return StatusCode(500, new { message = "An error occurred during TRL determination.", detail = ex.Message });
        }
    }

    // ─────────────────────────────────────────────────────────────────────────
    // ERB / SIA Initial Assessment
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns AI-powered initial ERB/SIA functional area impact predictions for a WSM request.
    /// Predicts which functional areas are likely impacted with confidence scores and reasoning.
    /// </summary>
    /// <param name="wsmRequestId">The GUID of the WSM request.</param>
    [HttpGet("wsm/{wsmRequestId:guid}/initial-erb-sia")]
    public async Task<IActionResult> GetInitialErbSiaAssessment(Guid wsmRequestId)
    {
        try
        {
            var result = await _aiService.GetInitialErbSiaAssessmentAsync(wsmRequestId);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate initial ERB/SIA assessment for WSM {WsmId}.", wsmRequestId);
            return StatusCode(500, new { message = "An error occurred during ERB/SIA assessment.", detail = ex.Message });
        }
    }
}
