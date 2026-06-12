using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PointScore.Data;
using PointScore.Models;
using PointScore.Models.DTOs;

namespace PointScore.Services;

/// <summary>
/// AI service that interfaces with OpenAI to provide TRL determination,
/// ERB/SIA assessment predictions, chat assistant, and CDRL draft generation.
/// Falls back to smart mock responses when no OpenAI API key is configured.
/// </summary>
public class AiService : IAiService
{
    private readonly CoreDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<AiService> _logger;

    // TRL walk-through questions (DoD standard definitions)
    private static readonly List<TrlWalkThroughQuestionDto> TrlQuestions = new()
    {
        new() { Level = 1, Question = "Has the basic scientific principle been observed and reported?",
            Context = "Basic principles have been observed and fundamental research is underway." },
        new() { Level = 2, Question = "Has a technology concept or application been formulated?",
            Context = "Application of basic principles to target area has been identified but is speculative." },
        new() { Level = 3, Question = "Has an analytical or experimental proof of concept been conducted?",
            Context = "Active research has validated the technology concept through analytical or laboratory studies." },
        new() { Level = 4, Question = "Has a technology component been validated in a laboratory environment?",
            Context = "Basic technological components have been integrated and tested in a laboratory." },
        new() { Level = 5, Question = "Has a technology component been validated in a relevant environment?",
            Context = "Fidelity of technology has been tested in simulated operational environment." },
        new() { Level = 6, Question = "Has a technology system or subsystem model been demonstrated in a relevant environment?",
            Context = "Representative model or prototype demonstrated in relevant operational environment." },
        new() { Level = 7, Question = "Has a technology prototype been demonstrated in an operational environment?",
            Context = "Prototype near or at planned operational system tested in operational environment." },
        new() { Level = 8, Question = "Has the actual technology system been completed and qualified through test and demonstration?",
            Context = "Technology proven to work in its final form and under expected conditions." },
        new() { Level = 9, Question = "Has the actual technology system been proven through successful mission operations?",
            Context = "Actual application of technology in its final form and under mission conditions." },
    };

    // TRL definitions (standard DoD/NASA)
    private static readonly Dictionary<int, string> TrlDefinitions = new()
    {
        { 1, "Basic principles observed and reported." },
        { 2, "Technology concept and/or application formulated." },
        { 3, "Analytical and experimental critical function and/or characteristic proof of concept." },
        { 4, "Component and/or breadboard validation in laboratory environment." },
        { 5, "Component and/or breadboard validation in relevant environment." },
        { 6, "System/subsystem model or prototype demonstration in a relevant environment." },
        { 7, "System prototype demonstration in an operational environment." },
        { 8, "System complete and qualified." },
        { 9, "Actual system proven in mission operations." },
    };

    public AiService(
        CoreDbContext context,
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory,
        ILogger<AiService> logger)
    {
        _context = context;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    // ─────────────────────────────────────────────────────────────────────────
    // TRL DETERMINATION
    // ─────────────────────────────────────────────────────────────────────────

    public async Task<AiTrlDeterminationResponseDto> DetermineTrlAsync(Guid wsmRequestId)
    {
        // 1. Load all relevant WSM data
        var wsm = await _context.WsmRequests
            .Include(w => w.Originator)
            .Include(w => w.ImpactAnalysis)
            .Include(w => w.ChangeDrivers).ThenInclude(cd => cd.ChangeDriverType)
            .Include(w => w.ModificationType)
            .Include(w => w.SiaScore)
            .Include(w => w.DetailedSiaResponses).ThenInclude(r => r.Section).ThenInclude(s => s.FunctionalArea)
            .Include(w => w.MRLResponses).ThenInclude(r => r.MRLSubThread)
            .FirstOrDefaultAsync(w => w.Id == wsmRequestId)
            ?? throw new KeyNotFoundException($"WSM Request with ID '{wsmRequestId}' was not found in the database.");

        // 2. Build structured context for the AI prompt
        var contextPayload = BuildTrlContextPayload(wsm);

        var apiKey = _configuration["OpenAI:ApiKey"];
        var model = _configuration["OpenAI:Model"] ?? "gpt-4o";

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            _logger.LogWarning("OpenAI API key not configured. Returning mocked TRL determination for WSM {WsmId}.", wsmRequestId);
            return BuildMockedTrlResponse(wsm);
        }

        try
        {
            var response = await CallOpenAiForTrlAsync(contextPayload, apiKey, model, wsmRequestId, wsm.RequestNumber, wsm.SystemName);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "OpenAI call failed for TRL determination on WSM {WsmId}. Falling back to mock.", wsmRequestId);
            return BuildMockedTrlResponse(wsm);
        }
    }

    private string BuildTrlContextPayload(WsmRequest wsm)
    {
        var sb = new StringBuilder();
        sb.AppendLine("=== WSM FORM DATA ===");
        sb.AppendLine($"Request Number: {wsm.RequestNumber}");
        sb.AppendLine($"System Name: {wsm.SystemName}");
        sb.AppendLine($"Title: {wsm.Title}");
        sb.AppendLine($"Description of Work: {wsm.Description}");
        sb.AppendLine($"Modification Type: {wsm.ModificationType?.Name ?? "Unknown"}");

        if (wsm.ChangeDrivers.Any())
        {
            sb.AppendLine($"Change Drivers: {string.Join(", ", wsm.ChangeDrivers.Select(cd => cd.ChangeDriverType?.Name ?? "Unknown"))}");
        }

        if (wsm.ImpactAnalysis != null)
        {
            sb.AppendLine("=== IMPACT ANALYSIS ===");
            var ia = wsm.ImpactAnalysis;
            var impactFields = new List<string>();
            if (ia.Software) impactFields.Add("Software");
            if (ia.Cyber) impactFields.Add("Cyber");
            if (ia.Safety) impactFields.Add("Safety");
            if (ia.Training) impactFields.Add("Training");
            if (ia.Requirements) impactFields.Add("Requirements");
            if (ia.ProductionLine) impactFields.Add("Production Line");
            if (impactFields.Any())
                sb.AppendLine($"Impacted Areas: {string.Join(", ", impactFields)}");
        }

        if (wsm.SiaScore != null)
        {
            sb.AppendLine("=== INITIAL SIA ASSESSMENT ===");
            var sia = wsm.SiaScore;
            sb.AppendLine($"Engineering Areas Score (EA): {sia.EA_Score:F2}");
            sb.AppendLine($"Other Functional Areas Score (OFA): {sia.OFA_Score:F2}");
            sb.AppendLine($"Normalized Engineering Score (NE): {sia.NE_Score:F2}");
            sb.AppendLine($"Normalized Other FA Score (NOFA): {sia.NOFA_Score:F2}");
            sb.AppendLine($"Software Score: {sia.Software_Score:F2}, Weight: {sia.Software_Weight:F2}");
            sb.AppendLine($"Cyber Score: {sia.Cyber_Score:F2}, Weight: {sia.Cyber_Weight:F2}");
            sb.AppendLine($"Safety Score: {sia.Safety_Score:F2}, Weight: {sia.Safety_Weight:F2}");
            sb.AppendLine($"Systems Engineering Score: {sia.SystemsEngineering_Score:F2}, Weight: {sia.SystemsEngineering_Weight:F2}");
            sb.AppendLine($"Test Score: {sia.Test_Score:F2}, Weight: {sia.Test_Weight:F2}");
        }

        if (wsm.DetailedSiaResponses.Any())
        {
            sb.AppendLine("=== DETAILED SIA ASSESSMENT (Impacted Sections) ===");
            var impacted = wsm.DetailedSiaResponses
                .Where(r => r.IsImpacted == true)
                .GroupBy(r => r.Section?.FunctionalArea?.Name ?? "Unknown")
                .ToList();

            foreach (var group in impacted)
            {
                sb.AppendLine($"  {group.Key}: {string.Join(", ", group.Select(r => r.Section?.Title ?? ""))}");
            }
        }

        if (wsm.MRLResponses.Any())
        {
            sb.AppendLine("=== MRL ASSESSMENT ===");
            var mrlTotal = wsm.MRLResponses.Sum(r => r.Score);
            sb.AppendLine($"Total MRL Score: {mrlTotal} / 280 (higher = more mature)");
        }

        sb.AppendLine();
        sb.AppendLine("=== TRL DEFINITIONS (DoD Standard) ===");
        foreach (var (level, def) in TrlDefinitions)
            sb.AppendLine($"TRL {level}: {def}");

        sb.AppendLine();
        sb.AppendLine("=== TRL WALK-THROUGH QUESTIONS ===");
        foreach (var q in TrlQuestions)
            sb.AppendLine($"TRL {q.Level}: {q.Question}");

        return sb.ToString();
    }

    private async Task<AiTrlDeterminationResponseDto> CallOpenAiForTrlAsync(
        string contextPayload, string apiKey, string model, Guid wsmRequestId, string requestNumber, string systemName)
    {
        var systemPrompt = @"You are an expert Defense Acquisition TRL (Technology Readiness Level) analyst.
Analyze the provided WSM (Weapon System Modification) form data, SIA assessments, MRL scores, and TRL definitions.
Determine the most appropriate TRL level (1-9) for this modification.

Respond ONLY with a valid JSON object in this exact format:
{
  ""determinedLevel"": <integer 1-9>,
  ""confidence"": <float 0.0 to 1.0>,
  ""rationale"": ""<detailed explanation of the determination>"",
  ""evaluatedQuestions"": [
    { ""level"": <int>, ""question"": ""<text>"", ""meetsRequirement"": <true|false|null>, ""aiComment"": ""<brief comment>"" }
  ],
  ""supportingEvidence"": [""<evidence point 1>"", ""<evidence point 2>""]
}";

        var userMessage = $"Based on the following WSM data, determine the TRL level:\n\n{contextPayload}";

        var requestBody = new
        {
            model,
            messages = new[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = userMessage }
            },
            temperature = 0.3,
            max_tokens = 1500,
            response_format = new { type = "json_object" }
        };

        var client = _httpClientFactory.CreateClient("OpenAI");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var httpResponse = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
        httpResponse.EnsureSuccessStatusCode();

        var responseJson = await httpResponse.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(responseJson);

        var messageContent = doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? "{}";

        using var aiDoc = JsonDocument.Parse(messageContent);
        var root = aiDoc.RootElement;

        int level = root.TryGetProperty("determinedLevel", out var lv) ? lv.GetInt32() : 5;
        level = Math.Clamp(level, 1, 9);
        decimal score = MapTrlLevelToScore(level);

        var evaluatedQuestions = new List<TrlEvaluatedQuestionDto>();
        if (root.TryGetProperty("evaluatedQuestions", out var eqArray))
        {
            foreach (var eq in eqArray.EnumerateArray())
            {
                evaluatedQuestions.Add(new TrlEvaluatedQuestionDto
                {
                    Level = eq.TryGetProperty("level", out var eqLv) ? eqLv.GetInt32() : 0,
                    Question = eq.TryGetProperty("question", out var eqQ) ? eqQ.GetString() ?? "" : "",
                    MeetsRequirement = eq.TryGetProperty("meetsRequirement", out var eqM) && eqM.ValueKind != JsonValueKind.Null
                        ? eqM.GetBoolean() : null,
                    AiComment = eq.TryGetProperty("aiComment", out var eqC) ? eqC.GetString() : null
                });
            }
        }

        var supportingEvidence = new List<string>();
        if (root.TryGetProperty("supportingEvidence", out var seArray))
        {
            foreach (var se in seArray.EnumerateArray())
                supportingEvidence.Add(se.GetString() ?? "");
        }

        return new AiTrlDeterminationResponseDto
        {
            WsmRequestId = wsmRequestId,
            RequestNumber = requestNumber,
            SystemName = systemName,
            DeterminedLevel = level,
            DeterminedScore = score,
            TrlDefinition = TrlDefinitions.GetValueOrDefault(level, ""),
            Rationale = root.TryGetProperty("rationale", out var rat) ? rat.GetString() ?? "" : "",
            Confidence = root.TryGetProperty("confidence", out var conf) ? conf.GetDouble() : 0.75,
            EvaluatedQuestions = evaluatedQuestions,
            SupportingEvidence = supportingEvidence,
            IsMocked = false,
            ModelVersion = model,
            GeneratedAt = DateTime.UtcNow
        };
    }

    private AiTrlDeterminationResponseDto BuildMockedTrlResponse(WsmRequest wsm)
    {
        // Smart mock: use available data to estimate TRL
        int level = EstimateTrlFromData(wsm);
        decimal score = MapTrlLevelToScore(level);

        return new AiTrlDeterminationResponseDto
        {
            WsmRequestId = wsm.Id,
            RequestNumber = wsm.RequestNumber,
            SystemName = wsm.SystemName,
            DeterminedLevel = level,
            DeterminedScore = score,
            TrlDefinition = TrlDefinitions.GetValueOrDefault(level, ""),
            Rationale = $"[Simulated] Based on the modification type '{wsm.ModificationType?.Name}', " +
                        $"description analysis, and available SIA data, the estimated TRL is {level}. " +
                        $"This is a simulated determination — configure OpenAI API key for live AI analysis.",
            Confidence = 0.65,
            EvaluatedQuestions = TrlQuestions
                .Select(q => new TrlEvaluatedQuestionDto
                {
                    Level = q.Level,
                    Question = q.Question,
                    MeetsRequirement = q.Level <= level ? true : q.Level == level + 1 ? null : false,
                    AiComment = q.Level <= level
                        ? "Requirement met based on available data."
                        : q.Level == level + 1
                        ? "Partially met — further evidence needed."
                        : "Not yet demonstrated at this level."
                })
                .ToList(),
            SupportingEvidence = new List<string>
            {
                $"Modification type: {wsm.ModificationType?.Name ?? "Unknown"}",
                $"Description length indicates detailed requirements specification.",
                $"Change drivers: {string.Join(", ", wsm.ChangeDrivers.Select(cd => cd.ChangeDriverType?.Name ?? "Unknown").Take(3))}",
                "SIA data available for initial assessment.",
                "MRL data available for manufacturing readiness cross-reference."
            },
            IsMocked = true,
            ModelVersion = "mock-v1.0",
            GeneratedAt = DateTime.UtcNow
        };
    }

    private static int EstimateTrlFromData(WsmRequest wsm)
    {
        // Smart heuristic: base level on modification type and available data
        var modType = wsm.ModificationType?.Name?.ToLower() ?? "";

        if (modType.Contains("maintenance"))
            return 8; // Maintenance fixes imply technology is mature
        if (modType.Contains("permanent"))
            return 6; // Permanent modifications likely in relevant environment
        if (modType.Contains("temporary"))
            return 5; // Temporary = validated but not permanent

        // Use SIA score as secondary indicator
        if (wsm.SiaScore != null)
        {
            var neScore = wsm.SiaScore.NE_Score;
            if (neScore >= 8) return 7;
            if (neScore >= 5) return 5;
            return 4;
        }

        return 5; // Default baseline
    }

    private static decimal MapTrlLevelToScore(int level)
    {
        // Maps TRL 1-9 to a normalized score 0-10
        return Math.Round((decimal)level / 9.0m * 10.0m, 2);
    }

    // ─────────────────────────────────────────────────────────────────────────
    // ERB/SIA ASSESSMENT
    // ─────────────────────────────────────────────────────────────────────────

    public async Task<AiErbSiaAssessmentDto> GetInitialErbSiaAssessmentAsync(Guid wsmRequestId)
    {
        var wsm = await _context.WsmRequests
            .Include(w => w.ImpactAnalysis)
            .Include(w => w.ChangeDrivers).ThenInclude(cd => cd.ChangeDriverType)
            .Include(w => w.ModificationType)
            .FirstOrDefaultAsync(w => w.Id == wsmRequestId)
            ?? throw new KeyNotFoundException($"WSM Request {wsmRequestId} not found.");

        var functionalAreas = await _context.FunctionalAreas
            .Where(f => f.IsActive)
            .OrderBy(f => f.Id)
            .ToListAsync();

        var apiKey = _configuration["OpenAI:ApiKey"];
        var model = _configuration["OpenAI:Model"] ?? "gpt-4o";

        if (string.IsNullOrWhiteSpace(apiKey))
            return BuildMockedErbSiaResponse(wsm, functionalAreas);

        try
        {
            return await CallOpenAiForErbSiaAsync(wsm, functionalAreas, apiKey, model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "OpenAI ERB/SIA assessment failed for WSM {WsmId}. Falling back to mock.", wsmRequestId);
            return BuildMockedErbSiaResponse(wsm, functionalAreas);
        }
    }

    private async Task<AiErbSiaAssessmentDto> CallOpenAiForErbSiaAsync(
        WsmRequest wsm, List<FunctionalArea> areas, string apiKey, string model)
    {
        var systemPrompt = @"You are an expert defense acquisition analyst specializing in System Impact Assessments (SIA).
Given a WSM (Weapon System Modification) request, predict which functional areas are likely impacted.
Return ONLY a valid JSON object:
{
  ""summaryNarrative"": ""<2-3 sentence executive summary>"",
  ""predictions"": [
    { ""functionalAreaId"": <int>, ""functionalAreaName"": ""<name>"", ""isImpacted"": <bool>, ""confidence"": <float 0-1>, ""reasoning"": ""<brief>"" }
  ]
}";

        var areaList = string.Join("\n", areas.Select(a => $"- ID {a.Id}: {a.Name}"));
        var userMessage = $@"WSM Request: {wsm.RequestNumber}
System: {wsm.SystemName}
Title: {wsm.Title}
Description: {wsm.Description}
Modification Type: {wsm.ModificationType?.Name}
Change Drivers: {string.Join(", ", wsm.ChangeDrivers.Select(cd => cd.ChangeDriverType?.Name ?? ""))}

Functional Areas to evaluate:
{areaList}

Predict impact for each functional area.";

        var requestBody = new
        {
            model,
            messages = new[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = userMessage }
            },
            temperature = 0.4,
            max_tokens = 1200,
            response_format = new { type = "json_object" }
        };

        var client = _httpClientFactory.CreateClient("OpenAI");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var httpResponse = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
        httpResponse.EnsureSuccessStatusCode();

        var responseJson = await httpResponse.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(responseJson);
        var messageContent = doc.RootElement
            .GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "{}";

        using var aiDoc = JsonDocument.Parse(messageContent);
        var root = aiDoc.RootElement;

        var predictions = new List<AiFunctionalAreaPredictionDto>();
        if (root.TryGetProperty("predictions", out var predsArray))
        {
            foreach (var p in predsArray.EnumerateArray())
            {
                predictions.Add(new AiFunctionalAreaPredictionDto
                {
                    FunctionalAreaId = p.TryGetProperty("functionalAreaId", out var fid) ? fid.GetInt32() : 0,
                    FunctionalAreaName = p.TryGetProperty("functionalAreaName", out var fn) ? fn.GetString() ?? "" : "",
                    IsImpacted = p.TryGetProperty("isImpacted", out var ii) && ii.GetBoolean(),
                    Confidence = p.TryGetProperty("confidence", out var conf) ? conf.GetDouble() : 0.5,
                    Reasoning = p.TryGetProperty("reasoning", out var r) ? r.GetString() ?? "" : ""
                });
            }
        }

        return new AiErbSiaAssessmentDto
        {
            WsmRequestId = wsm.Id,
            RequestNumber = wsm.RequestNumber,
            SystemName = wsm.SystemName,
            Title = wsm.Title,
            FunctionalAreaPredictions = predictions,
            SummaryNarrative = root.TryGetProperty("summaryNarrative", out var sn) ? sn.GetString() ?? "" : "",
            IsMocked = false,
            ModelVersion = model,
            GeneratedAt = DateTime.UtcNow
        };
    }

    private AiErbSiaAssessmentDto BuildMockedErbSiaResponse(WsmRequest wsm, List<FunctionalArea> areas)
    {
        var ia = wsm.ImpactAnalysis;
        var predictions = areas.Select(a =>
        {
            bool isImpacted = a.Name switch
            {
                "Software" => ia?.Software ?? false,
                "Cyber" => ia?.Cyber ?? false,
                "Safety" => ia?.Safety ?? false,
                "Systems Engineering" => ia?.SystemsEngineering ?? false,
                "Test" => ia?.Test ?? false,
                "Logistics" => ia?.Logistics ?? false,
                "Quality" => ia?.Quality ?? false,
                "Acquisition (Contracts)" => ia?.AcquisitionContracts ?? false,
                "Finance" => ia?.Finance ?? false,
                "Program Management" => ia?.ProgramManagement ?? false,
                "Security" => ia?.Security ?? false,
                _ => false
            };

            return new AiFunctionalAreaPredictionDto
            {
                FunctionalAreaId = a.Id,
                FunctionalAreaName = a.Name,
                IsImpacted = isImpacted,
                Confidence = isImpacted ? 0.82 : 0.71,
                Reasoning = isImpacted
                    ? $"[Simulated] The modification's impact fields suggest {a.Name} involvement."
                    : $"[Simulated] No direct {a.Name} impact indicators found in the WSM form."
            };
        }).ToList();

        return new AiErbSiaAssessmentDto
        {
            WsmRequestId = wsm.Id,
            RequestNumber = wsm.RequestNumber,
            SystemName = wsm.SystemName,
            Title = wsm.Title,
            FunctionalAreaPredictions = predictions,
            SummaryNarrative = $"[Simulated] The WSM '{wsm.Title}' for system '{wsm.SystemName}' has been analyzed. " +
                               $"Based on the impact fields, {predictions.Count(p => p.IsImpacted)} functional areas are predicted to be impacted. " +
                               "Configure OpenAI API key for live AI-powered analysis.",
            IsMocked = true,
            ModelVersion = "mock-v1.0",
            GeneratedAt = DateTime.UtcNow
        };
    }

    // ─────────────────────────────────────────────────────────────────────────
    // AI CHAT ASSISTANT
    // ─────────────────────────────────────────────────────────────────────────

    public async Task<AiChatResponseDto> QueryAiAssistantAsync(string query, Guid? wsmRequestId)
    {
        var apiKey = _configuration["OpenAI:ApiKey"];
        var model = _configuration["OpenAI:Model"] ?? "gpt-4o";

        string? wsmContext = null;
        if (wsmRequestId.HasValue)
        {
            wsmContext = await BuildWsmChatContextAsync(wsmRequestId.Value);
        }

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return new AiChatResponseDto
            {
                Response = $"[Simulated Mode] This is a simulated response to: \"{query}\". " +
                           "Configure an OpenAI API key in appsettings.json to enable live GPT-4o responses.",
                IsMocked = true
            };
        }

        try
        {
            var systemPrompt = "You are an expert defense acquisition analyst assistant specializing in WSM (Weapon System Modification) scoring and analysis. " +
                               "Provide concise, accurate, and professional responses about acquisition scores, TRL/MRL levels, SIA assessments, and related topics.";

            if (!string.IsNullOrEmpty(wsmContext))
                systemPrompt += $"\n\nCurrent WSM Context:\n{wsmContext}";

            var requestBody = new
            {
                model,
                messages = new[]
                {
                    new { role = "system", content = systemPrompt },
                    new { role = "user", content = query }
                },
                temperature = 0.5,
                max_tokens = 800
            };

            var client = _httpClientFactory.CreateClient("OpenAI");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
            httpResponse.EnsureSuccessStatusCode();

            var responseJson = await httpResponse.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);
            var response = doc.RootElement
                .GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "";

            return new AiChatResponseDto { Response = response, IsMocked = false };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AI chat query failed.");
            return new AiChatResponseDto
            {
                Response = "An error occurred while processing your request. Please try again.",
                IsMocked = true
            };
        }
    }

    private async Task<string> BuildWsmChatContextAsync(Guid wsmRequestId)
    {
        var wsm = await _context.WsmRequests
            .Include(w => w.SiaScore)
            .Include(w => w.TrlLevel)
            .Include(w => w.ModificationType)
            .FirstOrDefaultAsync(w => w.Id == wsmRequestId);

        if (wsm == null) return "";

        var sb = new StringBuilder();
        sb.AppendLine($"WSM: {wsm.RequestNumber} - {wsm.SystemName}");
        sb.AppendLine($"Title: {wsm.Title}");
        sb.AppendLine($"Status: {wsm.ApprovalWorkflow?.WorkflowStatus ?? "Unknown"}");
        if (wsm.TrlLevel != null)
            sb.AppendLine($"TRL Level: {wsm.TrlLevel.Level} (Score: {wsm.TrlLevel.Score:F2})");
        if (wsm.SiaScore != null)
            sb.AppendLine($"SIA NE Score: {wsm.SiaScore.NE_Score:F2}, NOFA Score: {wsm.SiaScore.NOFA_Score:F2}");

        return sb.ToString();
    }

    // ─────────────────────────────────────────────────────────────────────────
    // CDRL GENERATOR
    // ─────────────────────────────────────────────────────────────────────────

    public async Task<AiCdrlDraftDto> GenerateCdrlDraftAsync(string prompt, Guid? wsmRequestId)
    {
        var apiKey = _configuration["OpenAI:ApiKey"];
        var model = _configuration["OpenAI:Model"] ?? "gpt-4o";

        string? wsmContext = null;
        if (wsmRequestId.HasValue)
        {
            wsmContext = await BuildWsmChatContextAsync(wsmRequestId.Value);
        }

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return new AiCdrlDraftDto
            {
                Explanation = "[Simulated] Generated mock CDRL from prompt. Configure OpenAI API key for live generation.",
                Code = "DI-MGMT-81650",
                Title = "Program Management Report (Simulated)",
                Description = $"This CDRL was generated in simulated mode based on: {prompt}",
                FunctionalArea = "IPTProgramManagement",
                WhenNeeded = "Monthly",
                EstimatedCost = 15000,
                IsMocked = true
            };
        }

        try
        {
            var systemPrompt = @"You are a defense acquisition CDRL specialist. Generate a CDRL (Contract Data Requirements List) draft from the user's description.
Return ONLY valid JSON:
{
  ""explanation"": ""<how you interpreted the requirement>"",
  ""code"": ""<DID number like DI-MGMT-81650>"",
  ""title"": ""<CDRL title>"",
  ""description"": ""<detailed description>"",
  ""functionalArea"": ""<role name>"",
  ""whenNeeded"": ""<schedule constraint>"",
  ""estimatedCost"": <number>
}";

            var userMessage = string.IsNullOrEmpty(wsmContext)
                ? prompt
                : $"WSM Context:\n{wsmContext}\n\nRequest:\n{prompt}";

            var requestBody = new
            {
                model,
                messages = new[]
                {
                    new { role = "system", content = systemPrompt },
                    new { role = "user", content = userMessage }
                },
                temperature = 0.5,
                max_tokens = 600,
                response_format = new { type = "json_object" }
            };

            var client = _httpClientFactory.CreateClient("OpenAI");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
            httpResponse.EnsureSuccessStatusCode();

            var responseJson = await httpResponse.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);
            var messageContent = doc.RootElement
                .GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "{}";

            using var aiDoc = JsonDocument.Parse(messageContent);
            var root = aiDoc.RootElement;

            return new AiCdrlDraftDto
            {
                Explanation = root.TryGetProperty("explanation", out var exp) ? exp.GetString() ?? "" : "",
                Code = root.TryGetProperty("code", out var code) ? code.GetString() ?? "" : "",
                Title = root.TryGetProperty("title", out var title) ? title.GetString() ?? "" : "",
                Description = root.TryGetProperty("description", out var desc) ? desc.GetString() ?? "" : "",
                FunctionalArea = root.TryGetProperty("functionalArea", out var fa) ? fa.GetString() ?? "" : "",
                WhenNeeded = root.TryGetProperty("whenNeeded", out var wn) ? wn.GetString() ?? "" : "",
                EstimatedCost = root.TryGetProperty("estimatedCost", out var ec) ? ec.GetDecimal() : 0,
                IsMocked = false
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CDRL generation failed.");
            return new AiCdrlDraftDto
            {
                Explanation = "Failed to generate CDRL. Please try again.",
                Code = "",
                Title = "",
                Description = "",
                FunctionalArea = "IPTProgramManagement",
                WhenNeeded = "",
                EstimatedCost = 0,
                IsMocked = true
            };
        }
    }
}
