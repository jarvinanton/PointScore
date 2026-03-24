using System;
using System.Linq;
using System.Threading.Tasks;
using PointScore.Models;
using PointScore.Models.DTOs;
using PointScore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointScore.Services;

/// <summary>
/// Controller for handling score calculations and data retrieval.
/// </summary>
[ApiController]
[Route("api")]
public class PointScoreController : ControllerBase
{
    private readonly CoreDbContext _context;
    private readonly ScoreCalculator _scoreCalculator;

    public PointScoreController(CoreDbContext context, ScoreCalculator scoreCalculator)
    {
        _context = context;
        _scoreCalculator = scoreCalculator;
    }




    /// <summary>
    /// Receives feature input data, saves it to the database, and returns the inputs.
    /// This endpoint simulates the core scoring process and persists the input data.
    /// </summary>
    /// <param name="inputDto">The feature score data to be saved.</param>
    /// <returns>A JSON object containing the input data and calculated outputs.</returns>
    [HttpPost("wsm-feature")]
    public async Task<IActionResult> WsmFeature([FromBody] FeatureScoreInputDto inputDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                );
            return BadRequest(new { message = "Validation failed", errors });
        }

         // 1. Validar la entrada del DTO
        if (inputDto == null)
            return BadRequest("Invalid request payload.");

        // 2. Validar destino (WsmRequestId o ProjectId)
    if (!inputDto.WsmRequestId.HasValue && (!inputDto.ProjectId.HasValue || inputDto.ProjectId.Value <= 0))
    {
        return BadRequest("Either 'WsmRequestId' or 'ProjectId' must be provided.");
    }

    if (inputDto.WsmRequestId.HasValue)
    {
        var wsmExists = await _context.WsmRequests.AnyAsync(w => w.Id == inputDto.WsmRequestId.Value);
        if (!wsmExists)
            return NotFound($"WSM Request not found with ID: {inputDto.WsmRequestId.Value}");

        var scoreExists = await _context.FeatureScoresResults
            .AnyAsync(fs => fs.WsmRequestId == inputDto.WsmRequestId.Value);

        if (scoreExists)
            return Conflict($"A FeatureScoreResult already exists for WSM Request ID: {inputDto.WsmRequestId.Value}.");
    }

    if (inputDto.ProjectId.HasValue && inputDto.ProjectId.Value > 0)
    {
        var projectExists = await _context.Projects.AnyAsync(p => p.Id == inputDto.ProjectId.Value);
        if (!projectExists)
            return NotFound($"Project not found with ID: {inputDto.ProjectId.Value}");

        var scoreExists = await _context.FeatureScoresResults
            .AnyAsync(fs => fs.ProjectId == inputDto.ProjectId.Value);

        if (scoreExists)
            return Conflict($"A FeatureScoreResult already exists for Project ID: {inputDto.ProjectId.Value}.");
    }

    // Primero, creamos la entidad a partir del DTO.
    var input = new FeatureScoreResult()
    {
        ProjectId = inputDto.ProjectId,
        WsmRequestId = inputDto.WsmRequestId,
        ENG_SIA = inputDto.ENG_SIA,
            ENG_SIA_WT = inputDto.ENG_SIA_WT,
            FNTL_SIA = inputDto.FNTL_SIA,
            FNTL_SIA_WT = inputDto.FNTL_SIA_WT,
            COST_IMP = inputDto.COST_IMP,
            COST_WT = inputDto.COST_WT,
            SCH_IMP = inputDto.SCH_IMP,
            SCH_IMP_WT = inputDto.SCH_IMP_WT,
            TECH_IMP = inputDto.TECH_IMP,
            TECH_WT = inputDto.TECH_WT,
            FNTL_IMP = inputDto.FNTL_IMP,
            FNTL_IMP_WT = inputDto.FNTL_IMP_WT,
            TRL = inputDto.TRL,
            TRL_WT = inputDto.TRL_WT,
            DELIV = inputDto.DELIV,
            DELIV_WT = inputDto.DELIV_WT,
            MRL = inputDto.MRL,
            MRL_WT = inputDto.MRL_WT,
            SRR = inputDto.SRR,
            SRR_WT = inputDto.SRR_WT,
            PDR = inputDto.PDR,
            PDR_WT = inputDto.PDR_WT,
            CDR = inputDto.CDR,
            CDR_WT = inputDto.CDR_WT,
            SRR_PDR_CDR_WT = inputDto.SRR_PDR_CDR_WT,
            INTERD = inputDto.INTERD,
            INTERD_WT = inputDto.INTERD_WT,
            SELFDEP = inputDto.SELFDEP,
            SELFDEP_WT = inputDto.SELFDEP_WT,
            ORI = inputDto.ORI,
            ORI_WT = inputDto.ORI_WT,
            USER_IMP = inputDto.USER_IMP,
            USER_IMP_WT = inputDto.USER_IMP_WT,
            TIME_CRIT = inputDto.TIME_CRIT,
            TIME_CRIT_WT = inputDto.TIME_CRIT_WT,
            HICAT_TECH_WT = inputDto.HICAT_TECH_WT,
            HICAT_FNTL_WT = inputDto.HICAT_FNTL_WT,
            HICAT_SCH_WT = inputDto.HICAT_SCH_WT,
            HICAT_USER_WT = inputDto.HICAT_USER_WT,
            HICAT_COST_WT = inputDto.HICAT_COST_WT,
            DEV_COST = (decimal?)inputDto.DEV_COST,
            PROD_COST = (decimal?)inputDto.PROD_COST,
            PROD_QTY = (decimal?)inputDto.PROD_QTY,
            TOT_PROD_COST = (decimal?)inputDto.TOT_PROD_COST,
            INSTALL_COST = (decimal?)inputDto.INSTALL_COST,
            //AI_RESPONSE = inputDto.AI_RESPONSE ?? string.Empty,  // Handle null case
        };

        // Ahora, se aplican los cálculos a la entidad completa.
        input.HICAT_TECH_SCORE = _scoreCalculator.CalculateHighCategoryTechnicalScore(input);
        input.HICAT_FNTL_SCORE = _scoreCalculator.CalculateHighCategoryFunctionWorkScore(input);
        input.HICAT_SCH_SCORE = _scoreCalculator.CalculateHighCategoryScheduleScore(input);
        input.HICAT_USER_SCORE = _scoreCalculator.CalculateHighCategoryUserScore(input);
        input.HICAT_COST_SCORE = _scoreCalculator.CalculateHighCategoryCostScore(input);

        input.WSM_COMP_SCORE = _scoreCalculator.CalculateWsmCompositeScore(
            input.HICAT_TECH_SCORE ?? 0,
            input.HICAT_FNTL_SCORE ?? 0,
            input.HICAT_SCH_SCORE ?? 0,
            input.HICAT_USER_SCORE ?? 0);

        // decimal totalWsmCostPlaceholder = input.TOTAL_WSM_COST ?? 0;
        input.COST_CORR_SCORE = _scoreCalculator.CalculateCostCorrelatedScore(input.WSM_COMP_SCORE ?? 0, input);
        // input.RISK_TOL_LIKLI_SCORE = _scoreCalculator.CalculateRiskToleranceLikelihoodScore(score.HICAT_TECH_SCORE ?? 0);
        input.RISK_TOL_LIKLI_SCORE = _scoreCalculator.CalculateHighCategoryTechnicalScore(input);
        input.RISK_TOL_CONS_SCORE = _scoreCalculator.CalculateRiskToleranceConsequenceScore(input);
        input.TOTAL_WSM_COST= _scoreCalculator.CalculateTOTAL_WSM_COST(input);

        
        input.CreatedAt = DateTime.UtcNow;

        _context.FeatureScoresResults.Add(input);
        await _context.SaveChangesAsync();

        return Ok(input);
    }
    /// <summary>
/// Updates an existing feature score entry by ID.
/// </summary>
/// <param name="projectId">The ID of the project to update.</param>
/// <param name="inputDto">The updated feature score data.</param>
/// <returns>A JSON object containing the updated data and calculated outputs.</returns>
[HttpPut("wsm-feature/project/{projectId}")]
    public async Task<IActionResult> WsmFeature(int projectId, [FromBody] FeatureScoreInputDto inputDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                );
            return BadRequest(new { message = "Validation failed", errors });
        }

        // 1. Find the existing record in the database by its Project ID.
    var existingScore = await _context.FeatureScoresResults
                                  .FirstOrDefaultAsync(fs => fs.ProjectId == projectId);

    // 2. If the record is not found, return a 404 (Not Found) error.
    if (existingScore == null)
    {
        return NotFound($"Not found this record with Project ID: {projectId}");
    }

    // 3. Check if the ProjectId is being changed and validate it.
    if (inputDto.ProjectId.HasValue && inputDto.ProjectId.Value != projectId)
    {
        var newProjectId = inputDto.ProjectId.Value;

        // Verify that the new project exists.
        var newProjectExists = await _context.Projects.AnyAsync(p => p.Id == newProjectId);
        if (!newProjectExists)
        {
            return NotFound($"The new project with ID: {newProjectId} was not found.");
        }

        // Verify that the new project does not already have a feature score.
        var newProjectHasScore = await _context.FeatureScoresResults.AnyAsync(fs => fs.ProjectId == newProjectId);
        if (newProjectHasScore)
        {
            return Conflict($"The new project with ID: {newProjectId} is already associated with another feature score.");
        }

        // If validation passes, update the ProjectId.
        existingScore.ProjectId = newProjectId;
    }

    // 4. Update all other properties of the existing record with the data from the DTO.
    existingScore.ENG_SIA = inputDto.ENG_SIA;
    existingScore.ENG_SIA_WT = inputDto.ENG_SIA_WT;
    existingScore.FNTL_SIA = inputDto.FNTL_SIA;
    existingScore.FNTL_SIA_WT = inputDto.FNTL_SIA_WT;
    existingScore.COST_IMP = inputDto.COST_IMP;
    existingScore.COST_WT = inputDto.COST_WT;
    existingScore.SCH_IMP = inputDto.SCH_IMP;
    existingScore.SCH_IMP_WT = inputDto.SCH_IMP_WT;
    existingScore.TECH_IMP = inputDto.TECH_IMP;
    existingScore.TECH_WT = inputDto.TECH_WT;
    existingScore.FNTL_IMP = inputDto.FNTL_IMP;
    existingScore.FNTL_IMP_WT = inputDto.FNTL_IMP_WT;
    existingScore.TRL = inputDto.TRL;
    existingScore.TRL_WT = inputDto.TRL_WT;
    existingScore.DELIV = inputDto.DELIV;
    existingScore.DELIV_WT = inputDto.DELIV_WT;
    existingScore.MRL = inputDto.MRL;
    existingScore.MRL_WT = inputDto.MRL_WT;
    existingScore.SRR = inputDto.SRR;
    existingScore.SRR_WT = inputDto.SRR_WT;
    existingScore.PDR = inputDto.PDR;
    existingScore.PDR_WT = inputDto.PDR_WT;
    existingScore.CDR = inputDto.CDR;
    existingScore.CDR_WT = inputDto.CDR_WT;
    existingScore.SRR_PDR_CDR_WT = inputDto.SRR_PDR_CDR_WT;
    existingScore.INTERD = inputDto.INTERD;
    existingScore.INTERD_WT = inputDto.INTERD_WT;
    existingScore.SELFDEP = inputDto.SELFDEP;
    existingScore.SELFDEP_WT = inputDto.SELFDEP_WT;
    existingScore.ORI = inputDto.ORI;
    existingScore.ORI_WT = inputDto.ORI_WT;
    existingScore.USER_IMP = inputDto.USER_IMP;
    existingScore.USER_IMP_WT = inputDto.USER_IMP_WT;
    existingScore.TIME_CRIT = inputDto.TIME_CRIT;
    existingScore.TIME_CRIT_WT = inputDto.TIME_CRIT_WT;
    existingScore.HICAT_TECH_WT = inputDto.HICAT_TECH_WT;
    existingScore.HICAT_FNTL_WT = inputDto.HICAT_FNTL_WT;
    existingScore.HICAT_SCH_WT = inputDto.HICAT_SCH_WT;
    existingScore.HICAT_USER_WT = inputDto.HICAT_USER_WT;
    existingScore.HICAT_COST_WT = inputDto.HICAT_COST_WT;
    existingScore.DEV_COST = (decimal?)inputDto.DEV_COST;
    existingScore.PROD_COST = (decimal?)inputDto.PROD_COST;
    existingScore.PROD_QTY = (decimal?)inputDto.PROD_QTY;
    existingScore.TOT_PROD_COST = (decimal?)inputDto.TOT_PROD_COST;
    existingScore.INSTALL_COST = (decimal?)inputDto.INSTALL_COST;

    // 4. Recalculate all output scores with the updated data.
    existingScore.HICAT_TECH_SCORE = _scoreCalculator.CalculateHighCategoryTechnicalScore(existingScore);
    existingScore.HICAT_FNTL_SCORE = _scoreCalculator.CalculateHighCategoryFunctionWorkScore(existingScore);
    existingScore.HICAT_SCH_SCORE = _scoreCalculator.CalculateHighCategoryScheduleScore(existingScore);
    existingScore.HICAT_USER_SCORE = _scoreCalculator.CalculateHighCategoryUserScore(existingScore);
    existingScore.HICAT_COST_SCORE = _scoreCalculator.CalculateHighCategoryCostScore(existingScore);

    existingScore.WSM_COMP_SCORE = _scoreCalculator.CalculateWsmCompositeScore(
        existingScore.HICAT_TECH_SCORE ?? 0,
        existingScore.HICAT_FNTL_SCORE ?? 0,
        existingScore.HICAT_SCH_SCORE ?? 0,
        existingScore.HICAT_USER_SCORE ?? 0);

    existingScore.COST_CORR_SCORE = _scoreCalculator.CalculateCostCorrelatedScore(existingScore.HICAT_COST_SCORE ?? 0, existingScore);

    existingScore.RISK_TOL_LIKLI_SCORE = _scoreCalculator.CalculateHighCategoryTechnicalScore(existingScore);
    existingScore.RISK_TOL_CONS_SCORE = _scoreCalculator.CalculateRiskToleranceConsequenceScore(existingScore);
    existingScore.TOTAL_WSM_COST = _scoreCalculator.CalculateTOTAL_WSM_COST(existingScore);

    existingScore.CreatedAt = DateTime.UtcNow;

    // 5. Save the changes to the database.
    await _context.SaveChangesAsync();

    // 6. Return the updated record in the response.
    return Ok(existingScore);
}

    /// <summary>
    /// Retrieves a list of recent scores for the dashboard.
    /// </summary>
    /// <returns>A list of recent feature score inputs, including project name.</returns>
    [HttpGet("dashboard")]
    public async Task<IActionResult> GetScores()
    {
        // Joins with the Project entity to get the project name
        // and retrieves the last 10 records.
        var results = await _context.FeatureScoresResults
            .Include(fs => fs.Project) // Eager loads the related Project object
            .OrderByDescending(fs => fs.CreatedAt)
            .Take(10)
            .Select(f => new
            {
                // Include the project name for better dashboard visualization.
                ProjectName = f.Project != null ? f.Project.Name : "Unknown",
                WsmRequestId = f.WsmRequestId,
                // Now returning the actual calculated output metrics.
                WSM_COMP_SCORE = f.WSM_COMP_SCORE,
                HICAT_TECH_SCORE = f.HICAT_TECH_SCORE,
                HICAT_FNTL_SCORE = f.HICAT_FNTL_SCORE,
                HICAT_SCH_SCORE = f.HICAT_SCH_SCORE,
                HICAT_USER_SCORE = f.HICAT_USER_SCORE,
                HICAT_COST_SCORE = f.HICAT_COST_SCORE
            })
            .ToListAsync();

        return Ok(results);
    }

    /// <summary>
    /// Retrieves the most recent score data for external module reporting.
    /// </summary>
    /// <returns>A JSON object with recent score data formatted for various modules.</returns>
    [HttpGet("external-module")]
    public async Task<IActionResult> GetExternalModules()
    {
        // Gets the most recent FeatureScore record along with its associated Project
        var lastResult = await _context.FeatureScoresResults
            .Include(fs => fs.Project)
            .OrderByDescending(f => f.Id)
            .FirstOrDefaultAsync();

        if (lastResult == null)
            return NotFound("No data available.");

        // Using correct variable names as defined in the document.
        var response = new
        {
            CCARS_Module = new
            {
                ProjectName = lastResult.Project?.Name ?? "Unknown",
                CostDataRequirements = lastResult.COST_IMP,
                WsmPriorityScoring = lastResult.DELIV,
                MetricOutputs = lastResult.ENG_SIA
            },
            CDD_Reporting_Section = new
            {
                ProjectName = lastResult.Project?.Name ?? "Unknown",
                UpdatesToScore = lastResult.FNTL_SIA,
                WsmPriorityScore = lastResult.DELIV
            },
            Windchill_Module = new
            {
                PlmSystem = "Windchill",
                ProjectName = lastResult.Project?.Name ?? "Unknown",
                WsmPriority = lastResult.DELIV,
                CostScore = lastResult.COST_IMP,
                MetricOutputs = lastResult.ENG_SIA
            },
            R_Form_Section = new
            {
                ProjectName = lastResult.Project?.Name ?? "Unknown",
                WsmPriority = lastResult.DELIV,
                CostScore = lastResult.COST_IMP,
                ScoreMetrics = lastResult.ENG_SIA
            }
        };

        return Ok(response);
    }

    /// <summary>
    /// Processes data from an Artificial Intelligence module.
    /// </summary>
    /// <param name="request">The AI request DTO.</param>
    /// <returns>A response confirming the AI data has been processed and stored.</returns>
    [HttpPost("artificial-intelligence")]
    public async Task<IActionResult> PostArtificialIntelligence([FromBody] AiRequestDto request)
    {
        if (request == null || request.UpdatedScoreData == null)
        {
            return BadRequest("Invalid request payload.");
        }

        // Crea un nuevo registro de FeatureScoreResult primero.
        var score = new FeatureScoreResult
        {
            CreatedAt = DateTime.UtcNow
            // Placeholder: Asume que 'UpdatedScoreData' es mapeado a las entradas
            // e.g., EngSIA = request.UpdatedScoreData.EngSIA, etc.
        };

        // Crea un nuevo Project y establece la relación de uno a uno.
        var newProject = new Project
        {
            Name = "AI Generated Project",
            FeatureScoreResult = score
        };

        _context.Projects.Add(newProject);
        await _context.SaveChangesAsync();

        // Construye la respuesta.
        var response = new AiResponseDto
        {
            AiRequest = request.AiResponse,
            ScoreData = score
        };

        return Ok(response);
    }
    /// <summary>
    /// Processes data from an MBSE (Model-Based Systems Engineering) module.
    /// </summary>
    /// <param name="request">The MBSE data request DTO.</param>
    /// <returns>A response confirming the MBSE data has been processed and stored.</returns>
    [HttpPost("mbse-process")]
    public async Task<IActionResult> ProcessMbse([FromBody] DataResponseDto request)
    {
        if (request == null)
            return BadRequest("Invalid request payload.");

        // Creamos un nuevo FeatureScoreResult y mapeamos los datos.
        var scoreResult = new FeatureScoreResult
        {
            HICAT_COST_SCORE = Math.Round(request.CostScore, 2),
            WSM_COMP_SCORE = Math.Round(request.Metrics, 2),
            CreatedAt = DateTime.UtcNow
        };

        // Creamos un nuevo Project y establecemos la relación de uno a uno.
        var newProject = new Project
        {
            Name = "MBSE Generated Project",
            FeatureScoreResult = scoreResult
        };

        _context.Projects.Add(newProject);
        await _context.SaveChangesAsync();

        // La respuesta ahora se basa en el FeatureScoreResult.
        var response = new
        {
            DataRequest = "MBSE Data Request",
            HICAT_COST_SCORE = scoreResult.HICAT_COST_SCORE,
            WSM_COMP_SCORE = scoreResult.WSM_COMP_SCORE
        };

        return Ok(response);
    }

    /// <summary>
    /// Calculates and saves SIA scores for a specific WSM.
    /// </summary>
    [HttpPost("{wsmRequestId}/calculate-sia")]
    public async Task<IActionResult> CalculateWsmSia(Guid wsmRequestId, [FromBody] SiaScoreInputDto input)
    {
        if (input == null) return BadRequest("Invalid payload");

        var wsm = await _context.WsmRequests.Include(w => w.SiaScore).FirstOrDefaultAsync(w => w.Id == wsmRequestId);
        if (wsm == null) return NotFound($"WSM with ID {wsmRequestId} not found.");

        // Calculate
        var output = _scoreCalculator.CalculateSiaScores(input);

        // Update or Create Entity
        if (wsm.SiaScore == null)
        {
            wsm.SiaScore = new WsmSiaScore { WsmRequestId = wsmRequestId };
            _context.WsmSiaScores.Add(wsm.SiaScore);
        }

        // Map Inputs
        wsm.SiaScore.Logistics_Score = input.Logistics_Score;
        wsm.SiaScore.Logistics_Weight = input.Logistics_Weight;
        wsm.SiaScore.ProductionGFE_Score = input.ProductionGFE_Score;
        wsm.SiaScore.ProductionGFE_Weight = input.ProductionGFE_Weight;
        wsm.SiaScore.Safety_Score = input.Safety_Score;
        wsm.SiaScore.Safety_Weight = input.Safety_Weight;
        wsm.SiaScore.Quality_Score = input.Quality_Score;
        wsm.SiaScore.Quality_Weight = input.Quality_Weight;
        wsm.SiaScore.Cyber_Score = input.Cyber_Score;
        wsm.SiaScore.Cyber_Weight = input.Cyber_Weight;
        wsm.SiaScore.Software_Score = input.Software_Score;
        wsm.SiaScore.Software_Weight = input.Software_Weight;
        wsm.SiaScore.SystemsEngineering_Score = input.SystemsEngineering_Score;
        wsm.SiaScore.SystemsEngineering_Weight = input.SystemsEngineering_Weight;
        wsm.SiaScore.Test_Score = input.Test_Score;
        wsm.SiaScore.Test_Weight = input.Test_Weight;
        wsm.SiaScore.AcquisitionContracts_Score = input.AcquisitionContracts_Score;
        wsm.SiaScore.AcquisitionContracts_Weight = input.AcquisitionContracts_Weight;
        wsm.SiaScore.Finance_Score = input.Finance_Score;
        wsm.SiaScore.Finance_Weight = input.Finance_Weight;
        wsm.SiaScore.ProgramManagement_Score = input.ProgramManagement_Score;
        wsm.SiaScore.ProgramManagement_Weight = input.ProgramManagement_Weight;
        wsm.SiaScore.Security_Score = input.Security_Score;
        wsm.SiaScore.Security_Weight = input.Security_Weight;

        // Map Outputs
        wsm.SiaScore.EA_Score = output.EA_Score;
        wsm.SiaScore.OFA_Score = output.OFA_Score;
        wsm.SiaScore.NE_Score = output.NE_Score;
        wsm.SiaScore.NOFA_Score = output.NOFA_Score;
        
        wsm.SiaScore.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        var detail = new SiaScoreDetailDto
        {
            Id = wsm.SiaScore.Id,
            WsmRequestId = wsm.SiaScore.WsmRequestId,
            Logistics_Score = wsm.SiaScore.Logistics_Score,
            Logistics_Weight = wsm.SiaScore.Logistics_Weight,
            ProductionGFE_Score = wsm.SiaScore.ProductionGFE_Score,
            ProductionGFE_Weight = wsm.SiaScore.ProductionGFE_Weight,
            Safety_Score = wsm.SiaScore.Safety_Score,
            Safety_Weight = wsm.SiaScore.Safety_Weight,
            Quality_Score = wsm.SiaScore.Quality_Score,
            Quality_Weight = wsm.SiaScore.Quality_Weight,
            Cyber_Score = wsm.SiaScore.Cyber_Score,
            Cyber_Weight = wsm.SiaScore.Cyber_Weight,
            Software_Score = wsm.SiaScore.Software_Score,
            Software_Weight = wsm.SiaScore.Software_Weight,
            SystemsEngineering_Score = wsm.SiaScore.SystemsEngineering_Score,
            SystemsEngineering_Weight = wsm.SiaScore.SystemsEngineering_Weight,
            Test_Score = wsm.SiaScore.Test_Score,
            Test_Weight = wsm.SiaScore.Test_Weight,
            AcquisitionContracts_Score = wsm.SiaScore.AcquisitionContracts_Score,
            AcquisitionContracts_Weight = wsm.SiaScore.AcquisitionContracts_Weight,
            Finance_Score = wsm.SiaScore.Finance_Score,
            Finance_Weight = wsm.SiaScore.Finance_Weight,
            ProgramManagement_Score = wsm.SiaScore.ProgramManagement_Score,
            ProgramManagement_Weight = wsm.SiaScore.ProgramManagement_Weight,
            Security_Score = wsm.SiaScore.Security_Score,
            Security_Weight = wsm.SiaScore.Security_Weight,
            EA_Score = wsm.SiaScore.EA_Score,
            OFA_Score = wsm.SiaScore.OFA_Score,
            NE_Score = wsm.SiaScore.NE_Score,
            NOFA_Score = wsm.SiaScore.NOFA_Score,
            UpdatedAt = wsm.SiaScore.UpdatedAt
        };

        return Ok(detail);
    }

    /// <summary>
    /// Retrieves saved SIA scores for a specific WSM.
    /// </summary>
    [HttpGet("{wsmRequestId}/sia-scores")]
    public async Task<IActionResult> GetWsmSiaScores(Guid wsmRequestId)
    {
        var siaScore = await _context.WsmSiaScores.FirstOrDefaultAsync(s => s.WsmRequestId == wsmRequestId);

        if (siaScore == null) return NotFound($"No SIA scores found for WSM ID {wsmRequestId}");

         var detail = new SiaScoreDetailDto
        {
            Id = siaScore.Id,
            WsmRequestId = siaScore.WsmRequestId,
            Logistics_Score = siaScore.Logistics_Score,
            Logistics_Weight = siaScore.Logistics_Weight,
            ProductionGFE_Score = siaScore.ProductionGFE_Score,
            ProductionGFE_Weight = siaScore.ProductionGFE_Weight,
            Safety_Score = siaScore.Safety_Score,
            Safety_Weight = siaScore.Safety_Weight,
            Quality_Score = siaScore.Quality_Score,
            Quality_Weight = siaScore.Quality_Weight,
            Cyber_Score = siaScore.Cyber_Score,
            Cyber_Weight = siaScore.Cyber_Weight,
            Software_Score = siaScore.Software_Score,
            Software_Weight = siaScore.Software_Weight,
            SystemsEngineering_Score = siaScore.SystemsEngineering_Score,
            SystemsEngineering_Weight = siaScore.SystemsEngineering_Weight,
            Test_Score = siaScore.Test_Score,
            Test_Weight = siaScore.Test_Weight,
            AcquisitionContracts_Score = siaScore.AcquisitionContracts_Score,
            AcquisitionContracts_Weight = siaScore.AcquisitionContracts_Weight,
            Finance_Score = siaScore.Finance_Score,
            Finance_Weight = siaScore.Finance_Weight,
            ProgramManagement_Score = siaScore.ProgramManagement_Score,
            ProgramManagement_Weight = siaScore.ProgramManagement_Weight,
            Security_Score = siaScore.Security_Score,
            Security_Weight = siaScore.Security_Weight,
            EA_Score = siaScore.EA_Score,
            OFA_Score = siaScore.OFA_Score,
            NE_Score = siaScore.NE_Score,
            NOFA_Score = siaScore.NOFA_Score,
            UpdatedAt = siaScore.UpdatedAt
        };

        return Ok(detail);
    }

    /// <summary>
    /// Retrieves saved weights for a specific WSM.
    /// Task 515: Endpoint to retrieve and format all input punctuation marks.
    /// </summary>
    [HttpGet("{wsmRequestId}/weights")]
    public async Task<IActionResult> GetWsmWeights(Guid wsmRequestId)
    {
        var weights = await _context.WsmWeights.FirstOrDefaultAsync(w => w.WsmRequestId == wsmRequestId);

        if (weights == null) return NotFound($"No weights found for WSM ID {wsmRequestId}");

        var detail = new
        {
            Id = weights.Id,
            WsmRequestId = weights.WsmRequestId,
            TechnicalWeight = weights.TechnicalWeight,
            FunctionalWeight = weights.FunctionalWeight,
            ScheduleWeight = weights.ScheduleWeight,
            UserImpactWeight = weights.UserImpactWeight,
            CostWeight = weights.CostWeight,
            CreatedAt = weights.CreatedAt,
            UpdatedAt = weights.UpdatedAt
        };

        return Ok(detail);
    }

    /// <summary>
    /// Inserts or updates scoring weights for a specific WSM.
    /// Task: Endpoint to insert the WSM Weights.
    /// </summary>
    [HttpPost("{wsmRequestId}/weights")]
    public async Task<IActionResult> UpsertWsmWeights(Guid wsmRequestId, [FromBody] WsmWeightInputDto input)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var requestExists = await _context.WsmRequests.AnyAsync(r => r.Id == wsmRequestId);
        if (!requestExists) return NotFound($"WSM Request with ID {wsmRequestId} not found.");

        var weights = await _context.WsmWeights.FirstOrDefaultAsync(w => w.WsmRequestId == wsmRequestId);

        if (weights == null)
        {
            weights = new WsmWeight { WsmRequestId = wsmRequestId };
            _context.WsmWeights.Add(weights);
        }
        else
        {
            weights.UpdatedAt = DateTime.UtcNow;
        }

        weights.TechnicalWeight = input.TechnicalWeight;
        weights.FunctionalWeight = input.FunctionalWeight;
        weights.ScheduleWeight = input.ScheduleWeight;
        weights.UserImpactWeight = input.UserImpactWeight;
        weights.CostWeight = input.CostWeight;

        await _context.SaveChangesAsync();

        var detail = new
        {
            Id = weights.Id,
            WsmRequestId = weights.WsmRequestId,
            TechnicalWeight = weights.TechnicalWeight,
            FunctionalWeight = weights.FunctionalWeight,
            ScheduleWeight = weights.ScheduleWeight,
            UserImpactWeight = weights.UserImpactWeight,
            CostWeight = weights.CostWeight,
            CreatedAt = weights.CreatedAt,
            UpdatedAt = weights.UpdatedAt
        };

        return Ok(detail);
    }
}