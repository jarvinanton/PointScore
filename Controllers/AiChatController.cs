using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointScore.Models.DTOs;
using PointScore.Services;

/// <summary>
/// Controller for AI chat assistant and CDRL generation endpoints.
/// Routes: POST /api/AiChat/query, POST /api/AiChat/generate-cdrl
/// </summary>
[ApiController]
[Route("api/AiChat")]
[Authorize]
public class AiChatController : ControllerBase
{
    private readonly IAiService _aiService;
    private readonly ILogger<AiChatController> _logger;

    public AiChatController(IAiService aiService, ILogger<AiChatController> logger)
    {
        _aiService = aiService;
        _logger = logger;
    }

    /// <summary>
    /// Sends a query to the AI assistant with optional WSM context.
    /// </summary>
    [HttpPost("query")]
    public async Task<IActionResult> QueryAiAssistant([FromBody] AiChatQueryDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Query))
            return BadRequest(new { message = "Query cannot be empty." });

        try
        {
            var result = await _aiService.QueryAiAssistantAsync(request.Query, request.WsmRequestId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AI chat query failed.");
            return StatusCode(500, new { message = "AI chat service error.", detail = ex.Message });
        }
    }

    /// <summary>
    /// Generates a CDRL draft from a natural language prompt using AI.
    /// </summary>
    [HttpPost("generate-cdrl")]
    public async Task<IActionResult> GenerateCdrlDraft([FromBody] AiCdrlGenerateRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Prompt))
            return BadRequest(new { message = "Prompt cannot be empty." });

        try
        {
            var result = await _aiService.GenerateCdrlDraftAsync(request.Prompt, request.WsmRequestId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CDRL generation failed.");
            return StatusCode(500, new { message = "CDRL generation service error.", detail = ex.Message });
        }
    }
}
