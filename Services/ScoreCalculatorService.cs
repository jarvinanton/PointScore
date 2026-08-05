using PointScore.Models;
using PointScore.Data;
using PointScore.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
// using EllipticCurve.Utils;

namespace PointScore.Services;

/// <summary>
/// Service class for calculating WSM feature scores based on input metrics.
/// </summary>
public class ScoreCalculator
{
    private readonly CoreDbContext _context;
    private readonly IDeliverableCatalogService _catalogService;
    private readonly IWsmInterfaceService _interfaceService;

    public ScoreCalculator(CoreDbContext context, IDeliverableCatalogService catalogService, IWsmInterfaceService interfaceService)
    {
        _context = context;
        _catalogService = catalogService;
        _interfaceService = interfaceService;
    }

    /// <summary>
    /// Helper to resolve the External Interdependency score for a WSM.
    /// INTERD = block-level interdependency scoring → GrandTotalScore
    /// Returns 0 if the WSM is not assigned to a block.
    /// </summary>
    private async Task<decimal> GetInterdependencyScoreAsync(Guid wsmRequestId)
    {
        var wsm = await _context.WsmRequests
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.Id == wsmRequestId);

        if (wsm == null)
            throw new KeyNotFoundException($"WSM Request with ID {wsmRequestId} not found.");

        if (wsm.BlockId != null)
        {
            var scoring = await _interfaceService.GetBlockInterdependencyScoringAsync(wsm.BlockId.Value);
            return scoring.GrandTotalScore;
        }

        return 0m;
    }

    /// <summary>
    /// Helper to resolve the Self-Dependency score (Internal Complexity) for a WSM.
    /// Returns the ScoreOutput from the related block's complexity assessment.
    /// </summary>
    private async Task<decimal> GetSelfDependencyScoreAsync(Guid wsmRequestId)
    {
        var wsm = await _context.WsmRequests
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.Id == wsmRequestId);

        if (wsm == null)
            throw new KeyNotFoundException($"WSM Request with ID {wsmRequestId} not found.");

        if (wsm.BlockId == null)
            return 0m;

        var scoring = await _interfaceService.GetBlockComplexityScoringAsync(wsm.BlockId.Value);
        return scoring.ScoreOutput;
    }
    /// <summary>
    /// Calculates the WSM Composite Score based on various high-level scores.
    /// WSM_COMP_SCORE = (HICAT_TECH_SCORE + HICAT_FNTL_SCORE + HICAT_SCH_SCORE)
    /// </summary>
    public decimal CalculateWsmCompositeScore(decimal HICAT_TECH_SCORE, decimal HICAT_FNTL_SCORE, decimal HICAT_SCH_SCORE)
    {
        return HICAT_TECH_SCORE + HICAT_FNTL_SCORE + HICAT_SCH_SCORE;
    }

    /// <summary>
    /// Calculates the Correlated Cost Score.
    /// COST_CORR_SCORE = (HICAT_COST_SCORE) / (TOTAL_WSM_COST)
    /// </summary>
    public decimal CalculateCostCorrelatedScore(decimal HICAT_COST_SCORE, FeatureScoreResult input)
    {
        decimal TOTAL_WSM_COST = CalculateTOTAL_WSM_COST(input);
        return TOTAL_WSM_COST > 0 ? HICAT_COST_SCORE / TOTAL_WSM_COST : 0;
    }

    
    /// <summary>
    /// Calculates the High Category Technical Score.
    /// HICAT_TECH_SCORE = ((HICAT_TECH_WT) * ((ENG_SIA * ENG_SIA_WT) + (TECH_IMP *
    ///                     TECH_WT ) +
    ///                    (TRL * TRL_WT) + (DELIV * DELIV_WT) + ((SRR_PDR_CDR_WT) * ((SRR * SRR_WT) + (PDR *
    ///                     PDR_WT) + (CDR * CDR_WT))) + ((INTERD * INTERD_WT) + (SELFDEP + SELFDEP_WT))))
    /// </summary>
    public decimal CalculateHighCategoryTechnicalScore(FeatureScoreResult input)
    {
        return (input.HICAT_TECH_WT ?? 0) * (
            ((input.ENG_SIA ?? 0) * (input.ENG_SIA_WT ?? 0)) + 
            ((input.TECH_IMP ?? 0) * (input.TECH_WT ?? 0)) +
            ((input.TRL ?? 0) * (input.TRL_WT ?? 0)) + 
            ((input.DELIV ?? 0) * (input.DELIV_WT ?? 0)) + 
            (input.SRR_PDR_CDR_WT ?? 0) * ((input.SRR ?? 0) * (input.SRR_WT ?? 0) + (input.PDR ?? 0) * (input.PDR_WT ?? 0) + (input.CDR ?? 0) * (input.CDR_WT ?? 0)) + 
            ((input.INTERD ?? 0) * (input.INTERD_WT ?? 0)) + ((input.SELFDEP ?? 0) + (input.SELFDEP_WT ?? 0))
        );
    }
    
    /// <summary>
    /// Calculates the High Category Functional Work Score.
    /// HICAT_FNTL_SCORE = ((HICAT_FNTL_WT) * ((FNTL_SIA * FNTL_SIA_WT) + (FNTL_IMP * FNTL_IMP_WT ))
    /// </summary>
    public decimal CalculateHighCategoryFunctionWorkScore(FeatureScoreResult input)
    {
        return (input.HICAT_FNTL_WT ?? 0) * (((input.FNTL_SIA ?? 0) * (input.FNTL_SIA_WT ?? 0)) + ((input.FNTL_IMP ?? 0) * (input.FNTL_IMP_WT ?? 0)));
    }

    /// <summary>
    /// Calculates the High Category Schedule Score.
    /// HICAT_SCH_SCORE = ((HICAT_SCH_WT)  *  ((SCH_IMP * SCH_IMP_WT) + (FNTL_IMP * FNTL_IMP_WT ) + (MRL * MRL_WT) + (TIME_CRIT * TIME_CRIT_WT))
    /// </summary>
    public decimal CalculateHighCategoryScheduleScore(FeatureScoreResult input)
    {
        return (input.HICAT_SCH_WT ?? 0) * ((input.SCH_IMP ?? 0) * (input.SCH_IMP_WT ?? 0)+(input.FNTL_IMP ?? 0) * (input.FNTL_IMP_WT ?? 0) + (input.MRL ?? 0) * (input.MRL_WT ?? 0) + (input.TIME_CRIT ?? 0) * (input.TIME_CRIT_WT ?? 0));
    }

    /// <summary>
    /// Calculates the High Category User Score.
    /// HICAT_USER_SCORE = ((HICAT_USER_WT) * ((ORI * ORI_WT) + (USER_IMP * USER_IMP_WT ))
    /// </summary>
    public decimal CalculateHighCategoryUserScore(FeatureScoreResult input)
    {
        return (input.HICAT_USER_WT ?? 0) * (((input.ORI ?? 0) * (input.ORI_WT ?? 0)) + ((input.USER_IMP ?? 0) * (input.USER_IMP_WT ?? 0)));
    }
    
    
    /// <summary>
    /// Calculates the High Category Cost Score.
    /// HICAT_COST_SCORE = ((HICAT_COST_WT) * ((COST_IMP * COST_IMP_WT) + (TRL * (TRL_WT/5)) + (TIME_CRIT * TIME_CRIT_WT) +                                                                                                     (TECH_IMP * (TECH_WT/3) ) + (DELIV * (DELIV_WT * 1.5) + 
    /// ((SRR_PDR_CDR_WT-0.1) * ((SRR * SRR_WT) + (PDR * PDR_WT) + (CDR * CDR_WT)) + ((INTERD * INTERD_WT) + (SELFDEP * (SELFDEP_WT/2))))))
    /// </summary>
    public decimal CalculateHighCategoryCostScore(FeatureScoreResult input)
    {
        return (input.HICAT_COST_WT ?? 0) * (
            ((input.COST_IMP ?? 0) * (input.COST_WT ?? 0)) + 
            ((input.MRL ?? 0) * (input.MRL_WT ?? 0)/8) +
            ((input.TRL ?? 0) * ((input.TRL_WT ?? 0) / 5)) + 
            ((input.TIME_CRIT ?? 0) * (input.TIME_CRIT_WT ?? 0)) + 
            ((input.TECH_IMP ?? 0) * ((input.TECH_WT ?? 0) / 3)) + 
            ((input.DELIV ?? 0) * ((input.DELIV_WT ?? 0) * 1.5m)) + 
            (((input.SRR_PDR_CDR_WT ?? 0) - 0.1m) * (
                ((input.SRR ?? 0) * (input.SRR_WT ?? 0)) + 
                ((input.PDR ?? 0) * (input.PDR_WT ?? 0)) + 
                ((input.CDR ?? 0) * (input.CDR_WT ?? 0))
            )) + 
            ((input.INTERD ?? 0) * (input.INTERD_WT ?? 0)) + 
            ((input.SELFDEP ?? 0) * ((input.SELFDEP_WT ?? 0) / 2))
        );
    }
    
    /// <summary>
    /// Calculates the Risk Tolerance Likelihood Score.
    /// RISK_TOL_LIKLI_SCORE = HICAT_TECH_SCORE
    /// </summary>
    public decimal CalculateRiskToleranceLikelihoodScore(decimal HICAT_TECH_SCORE)
    {
        return HICAT_TECH_SCORE;
    }
    
    /// <summary>
    /// Calculates the Risk Tolerance Consequence Score.
    /// RISK_TOL_CONS_SCORE = (INTERD * INTERD_WT) + (SELFDEP * SELFDEP_WT)
    /// </summary>
    public decimal CalculateRiskToleranceConsequenceScore(FeatureScoreResult input)
    {
        return ((input.INTERD ?? 0) * (input.INTERD_WT ?? 0)) + ((input.SELFDEP ?? 0) * (input.SELFDEP_WT ?? 0));
    }
    public decimal CalculateTOTAL_WSM_COST(FeatureScoreResult input)
    {
        return (input.PROD_COST ?? 0) * (input.PROD_QTY ?? 0);
    }

    /// <summary>
    /// Calculates SIA Scores (Engineering vs Other Functional Areas).
    /// </summary>
    public PointScore.Models.DTOs.SiaScoreOutputDto CalculateSiaScores(PointScore.Models.DTOs.SiaScoreInputDto input)
    {
        // 1. Engineering Areas (Safety, Quality, Cyber, Software, Systems Engineering, Test)
        decimal eaScore = input.Safety_Score + input.Quality_Score + input.Cyber_Score + 
                          input.Software_Score + input.SystemsEngineering_Score + input.Test_Score;
        
        decimal eaWeight = input.Safety_Weight + input.Quality_Weight + input.Cyber_Weight + 
                           input.Software_Weight + input.SystemsEngineering_Weight + input.Test_Weight;

        // 2. Other Functional Areas
        decimal ofaScore = input.Logistics_Score + input.ProductionGFE_Score + input.AcquisitionContracts_Score + 
                           input.Finance_Score + input.ProgramManagement_Score + input.Security_Score;
        
        decimal ofaWeight = input.Logistics_Weight + input.ProductionGFE_Weight + input.AcquisitionContracts_Weight + 
                            input.Finance_Weight + input.ProgramManagement_Weight + input.Security_Weight;

        // 3. Normalization (Avoid divide by zero)
        decimal neScore = eaWeight != 0 ? (eaScore / eaWeight) * 10 : 0;
        decimal nofaScore = ofaWeight != 0 ? (ofaScore / ofaWeight) * 10 : 0;
        return new PointScore.Models.DTOs.SiaScoreOutputDto
        {
            EA_Score = Math.Round(eaScore, 2),
            OFA_Score = Math.Round(ofaScore, 2),
            NE_Score = Math.Round(neScore, 2),
            NOFA_Score = Math.Round(nofaScore, 2)
        };
    }

    /// <summary>
    /// Calculates the granular breakdown of the High Category Technical Score
    /// using data directly retrieved from the database.
    /// </summary>
    public async Task<HighCategoryTechnicalScoreDto> CalculateHighCategoryTechnicalScoreDetailsAsync(Guid wsmRequestId)
    {
        var dto = new HighCategoryTechnicalScoreDto();
        decimal selfdepValue = await GetSelfDependencyScoreAsync(wsmRequestId);

        // 1. ENG_SIA
        var siaScore = await _context.WsmSiaScores.FirstOrDefaultAsync(s => s.WsmRequestId == wsmRequestId);
        dto.ENG_SIA_Score = siaScore?.NE_Score ?? 0m;

        // 2. TECH_IMP
        // Actual Cost Score = Sum of Weights for all impacted Detailed SIA Responses
        var actualCostScore = await _context.WsmDetailedSiaResponses
            .Include(r => r.Section)
            .ThenInclude(s => s.FunctionalArea)
            .Where(r => r.WsmRequestId == wsmRequestId && r.IsImpacted == true)
            .SumAsync(r => r.Section.FunctionalArea.Weight);

        // Max Weights Total = Sum of Weights for all active Detailed SIA Sections
        var maxWeightsTotal = await _context.DetailedSiaSections
            .Include(s => s.FunctionalArea)
            .Where(s => s.IsActive)
            .SumAsync(s => s.FunctionalArea.Weight);

        dto.TECH_IMP_Score = maxWeightsTotal > 0 ? (actualCostScore / maxWeightsTotal) * 10m : 0m;

        // 3. TRL
        var trl = await _context.TRL_levels.FirstOrDefaultAsync(t => t.WsmRequestId == wsmRequestId);
        dto.TRL_Score = trl?.Score ?? 0m;

        // 4. DELIV
        // Applicable WSM Score of all applicable deliverables for this WSM
        var assignedDeliverableCodes = await _context.WsmDeliverables
            .Include(d => d.Deliverable)
            .Where(d => d.WsmRequestId == wsmRequestId)
            .Select(d => d.Deliverable.Code)
            .ToListAsync();

        var catalogDeliverables = _catalogService.GetAllDeliverables();
        dto.DELIV_Score = catalogDeliverables
            .Where(d => assignedDeliverableCodes.Contains(d.DIDNumber) && d.IsApplicableWSM)
            .Sum(d => d.ApplicableWSMScore ?? 0m);

        // 5. SRR, PDR, CDR
        var milestones = await _context.WsmMilestoneScores
            .Where(m => m.WsmRequestId == wsmRequestId)
            .ToListAsync();

        dto.SRR_Score = milestones.FirstOrDefault(m => m.MilestoneType == "SRR")?.FinalScore ?? 0m;
        dto.PDR_Score = milestones.FirstOrDefault(m => m.MilestoneType == "PDR")?.FinalScore ?? 0m;
        dto.CDR_Score = milestones.FirstOrDefault(m => m.MilestoneType == "CDR")?.FinalScore ?? 0m;

        // 6. INTERD — from block-level interface interdependency scoring
        dto.INTERD_Score = await GetInterdependencyScoreAsync(wsmRequestId);

        // 7. SELFDEP — from block-level internal complexity scoring
        dto.SELFDEP_Score = selfdepValue;

        // Aggregate with sub-weights for SRR, PDR, CDR combined into SRR_PDR_CDR component
        decimal srrPdrCdrScore = (dto.SRR_Score * 0.25m) + (dto.PDR_Score * 0.35m) + (dto.CDR_Score * 0.40m);

        // Overall combination according to constant weights
        // sub-Weights: ENG_SIA (5%), TECH_IMP (15%), TRL (25%), DELIV (10%), SRR_PDR_CDR (25%), INTERD (10%), SELFDEP (10%)
        decimal rawTotalScore = 
            (dto.ENG_SIA_Score * 0.05m) +
            (dto.TECH_IMP_Score * 0.15m) +
            (dto.TRL_Score * 0.25m) +
            (dto.DELIV_Score * 0.10m) +
            (srrPdrCdrScore * 0.25m) +
            (dto.INTERD_Score * 0.10m) +
            (dto.SELFDEP_Score * 0.10m);

        // Apply constant overall weight of 15% (0.15) for High Category Technical Score
        dto.TotalHighCategoryTechnicalScore = Math.Round(rawTotalScore * 0.15m, 2);

        return dto;
    }

    /// <summary>
    /// Calculates the granular breakdown of the High Category Functional Work Score
    /// using data directly retrieved from the database and restricted to specific functional areas for the impact aspect.
    /// Task 521.
    /// </summary>
    public async Task<HighCategoryFunctionalScoreDto> CalculateHighCategoryFunctionalScoreDetailsAsync(Guid wsmRequestId)
    {
        var dto = new HighCategoryFunctionalScoreDto();

        // 1. FNTL_SIA (Functional Initial SIA) -> Mapped from NOFA_Score
        var siaScore = await _context.WsmSiaScores.FirstOrDefaultAsync(s => s.WsmRequestId == wsmRequestId);
        dto.FNTL_SIA_Score = siaScore?.NOFA_Score ?? 0m;

        // 2. FNTL_IMP (System Impact Assessment - Detailed ECP)
        // Restricted to specific areas per user requirement
        var allowedAreas = new[] { "Software", "Cyber", "Quality", "Safety", "Systems Engineering" };
        
        var actualCostScore = await _context.WsmDetailedSiaResponses
            .Include(r => r.Section)
            .ThenInclude(s => s.FunctionalArea)
            .Where(r => r.WsmRequestId == wsmRequestId && r.IsImpacted == true && allowedAreas.Contains(r.Section.FunctionalArea.Name))
            .SumAsync(r => r.Section.FunctionalArea.Weight);

        var maxWeightsTotal = await _context.DetailedSiaSections
            .Include(s => s.FunctionalArea)
            .Where(s => s.IsActive && allowedAreas.Contains(s.FunctionalArea.Name))
            .SumAsync(s => s.FunctionalArea.Weight);

        dto.FNTL_IMP_Score = maxWeightsTotal > 0 ? (actualCostScore / maxWeightsTotal) * 10m : 0m;

        // Overall combination according to constant weights
        // sub-Weights: FNTL_SIA (30%), FNTL_IMP (70%)
        decimal rawTotalScore = 
            (dto.FNTL_SIA_Score * 0.30m) +
            (dto.FNTL_IMP_Score * 0.70m);

        // Apply constant overall weight of 15% (0.15) for High Category Functional Work Score
        dto.TotalHighCategoryFunctionalScore = Math.Round(rawTotalScore * 0.15m, 2);

        return dto;
    }

    /// <summary>
    /// Calculates the granular breakdown of the High Category User Score
    /// using data direct from WsmOriAssessment and WsmMissionImpactAssessment.
    /// Task 525 / Task 511.
    /// </summary>
    public async Task<HighCategoryUserScoreDto> CalculateHighCategoryUserScoreDetailsAsync(Guid wsmRequestId)
    {
        var dto = new HighCategoryUserScoreDto();

        // 1. Operational Readiness Impact (ORI) -> From WsmOriAssessment
        var oriAssessment = await _context.WsmOriAssessments.FirstOrDefaultAsync(o => o.WsmRequestId == wsmRequestId);
        dto.OperationalReadinessImpact_Score = (decimal)(oriAssessment?.Score ?? 0);

        // 2. User Mission Impact -> From WsmMissionImpactAssessment (Task 511)
        var missionImpactAssessment = await _context.WsmMissionImpactAssessments.FirstOrDefaultAsync(m => m.WsmRequestId == wsmRequestId);
        dto.UserMissionImpact_Score = (decimal)(missionImpactAssessment?.Score ?? 0);

        // Overall combination according to constant weights
        // sub-Weights: ORI (50%), Mission Impact (50%)
        decimal rawTotalScore = 
            (dto.OperationalReadinessImpact_Score * 0.50m) +
            (dto.UserMissionImpact_Score * 0.50m);

        // Apply constant overall weight of 50% (0.50) for High Category User Score
        dto.TotalHighCategoryUserScore = Math.Round(rawTotalScore * 0.50m, 2);

        return dto;
    }

    /// <summary>
    /// Calculates the granular breakdown of the High Category Schedule Score
    /// using data direct from database (MRL, Time Crit) and an input for SIA Tech Detail ECP.
    /// Task 523.
    /// </summary>
    public async Task<HighCategoryScheduleScoreDto> CalculateHighCategoryScheduleScoreDetailsAsync(Guid wsmRequestId)
    {
        var dto = new HighCategoryScheduleScoreDto();

        // 1. SIA Tech Detail ECP (SCH_IMP) -> From aggregate scoring logic / 10
        var siaSummary = await CalculateDetailedSiaSummaryAsync(wsmRequestId);
        dto.SiaTechDetailEcp_Score = (decimal)siaSummary.TotalWsmScheduleScore / 10m;

        // 2. Manufacturing Readiness Level (MRL) -> (280 - score) / 280 * 10
        var mrlScoreTotal = await _context.MRLResponses
            .Where(m => m.WsmRequestId == wsmRequestId)
            .SumAsync(m => m.Score);
        
        // 280 is the constant representing the total possible MRL sub-thread score
        dto.ManufacturingReadinessLevel_Score = (280m - (decimal)mrlScoreTotal) / 280m * 10m;

        // 3. Time Criticality -> From WsmTimeCriticalityAssessment
        var timeCritAssessment = await _context.WsmTimeCriticalityAssessments.FirstOrDefaultAsync(t => t.WsmRequestId == wsmRequestId);
        dto.TimeCriticality_Score = (decimal)(timeCritAssessment?.Score ?? 0);

        // Overall combination according to constant weights
        // sub-Weights: SIA (40%), MRL (40%), Time Criticality (20%)
        decimal rawTotalScore = 
            (dto.SiaTechDetailEcp_Score * 0.40m) +
            (dto.ManufacturingReadinessLevel_Score * 0.40m) +
            (dto.TimeCriticality_Score * 0.20m);

        // Apply constant overall weight of 20% (0.20) for High Category Schedule Score
        dto.TotalHighCategoryScheduleScore = Math.Round(rawTotalScore * 0.20m, 2);

        return dto;
    }

    /// <summary>
    /// Calculates the granular breakdown of the High Category Cost Score
    /// using data from various models and confirmed formulas.
    /// Task 527.
    /// </summary>
    public async Task<HighCategoryCostScoreDto> CalculateHighCategoryCostScoreDetailsAsync(Guid wsmRequestId)
    {
        var dto = new HighCategoryCostScoreDto();
        decimal selfdepValue = await GetSelfDependencyScoreAsync(wsmRequestId);

        // 1. SIA Tech Detail ECP (5%) -> NE_Score (from WsmSiaScores)
        var siaScore = await _context.WsmSiaScores.FirstOrDefaultAsync(s => s.WsmRequestId == wsmRequestId);
        dto.SiaTechDetailEcp_Score = siaScore?.NE_Score ?? 0m;

        // 2. TRL (5%) -> From TRL_levels
        var trl = await _context.TRL_levels.FirstOrDefaultAsync(t => t.WsmRequestId == wsmRequestId);
        dto.TRL_Score = trl?.Score ?? 0m;

        // 3. Deliverables (15%) -> Sum of applicable WSM scores
        var assignedDeliverableCodes = await _context.WsmDeliverables
            .Include(d => d.Deliverable)
            .Where(d => d.WsmRequestId == wsmRequestId)
            .Select(d => d.Deliverable.Code)
            .ToListAsync();
        var catalogDeliverables = _catalogService.GetAllDeliverables();
        dto.Deliverables_Score = catalogDeliverables
            .Where(d => assignedDeliverableCodes.Contains(d.DIDNumber) && d.IsApplicableWSM)
            .Sum(d => d.ApplicableWSMScore ?? 0m);

        // 4. MRL (5%) -> (280 - score) / 280 * 10
        var mrlScoreTotal = await _context.MRLResponses
            .Where(m => m.WsmRequestId == wsmRequestId)
            .SumAsync(m => m.Score);
        dto.ManufacturingReadinessLevel_Score = (280m - (decimal)mrlScoreTotal) / 280m * 10m;

        // 5. SRR / PDR / CDR (15%) -> Weighted sum: SRR (25%), PDR (35%), CDR (40%)
        var milestones = await _context.WsmMilestoneScores
            .Where(m => m.WsmRequestId == wsmRequestId)
            .ToListAsync();
        decimal srrScore = milestones.FirstOrDefault(m => m.MilestoneType == "SRR")?.FinalScore ?? 0m;
        decimal pdrScore = milestones.FirstOrDefault(m => m.MilestoneType == "PDR")?.FinalScore ?? 0m;
        decimal cdrScore = milestones.FirstOrDefault(m => m.MilestoneType == "CDR")?.FinalScore ?? 0m;
        dto.SrrPdrCdr_Score = (srrScore * 0.25m) + (pdrScore * 0.35m) + (cdrScore * 0.40m);

        // 6. Interdependency Interop (10%) -> Formula (280 - MRL_Total) / 280 * 10
        dto.InterdependencyInterop_Score = dto.ManufacturingReadinessLevel_Score;

        // 7. Interdependency Self (5%) -> Automated from block complexity
        dto.InterdependencySelf_Score = selfdepValue;

        // 8. Time Criticality (10%) -> From WsmTimeCriticalityAssessment
        var timeCritAssessment = await _context.WsmTimeCriticalityAssessments.FirstOrDefaultAsync(t => t.WsmRequestId == wsmRequestId);
        dto.TimeCriticality_Score = (decimal)(timeCritAssessment?.Score ?? 0);

        // 9. SIA COST Detail ECP (30%) -> Actual Cost / Max Weight * 10
        // (Summatory of ALL areas confirmed by user)
        var actualCostScore = await _context.WsmDetailedSiaResponses
            .Include(r => r.Section)
            .ThenInclude(s => s.FunctionalArea)
            .Where(r => r.WsmRequestId == wsmRequestId && r.IsImpacted == true)
            .SumAsync(r => r.Section.FunctionalArea.Weight);

        var maxWeightsTotal = await _context.DetailedSiaSections
            .Include(s => s.FunctionalArea)
            .Where(s => s.IsActive)
            .SumAsync(s => s.FunctionalArea.Weight);

        dto.SiaCostDetailEcp_Score = maxWeightsTotal > 0 ? (actualCostScore / maxWeightsTotal) * 10m : 0m;

        // Overall combination
        decimal rawTotalScore = 
            (dto.SiaTechDetailEcp_Score * 0.05m) +
            (dto.TRL_Score * 0.05m) +
            (dto.Deliverables_Score * 0.15m) +
            (dto.ManufacturingReadinessLevel_Score * 0.05m) +
            (dto.SrrPdrCdr_Score * 0.15m) +
            (dto.InterdependencyInterop_Score * 0.10m) +
            (dto.InterdependencySelf_Score * 0.05m) +
            (dto.TimeCriticality_Score * 0.10m) +
            (dto.SiaCostDetailEcp_Score * 0.30m);

        // Apply Cost Category weight (75%)
        dto.TotalHighCategoryCostScore = Math.Round(rawTotalScore * 0.75m, 2);

        return dto;
    }

    /// <summary>
    /// Calculates the final Correlated Cost Score (COST_CORR_SCORE) and all category aggregates.
    /// WSM_COMP_SCORE = Tech + Functional + Schedule
    /// COST_CORR_SCORE = (HICAT_COST_SCORE) / (TOTAL_WSM_COST)
    /// TOTAL_WSM_COST = TOT_PROD_COST * PROD_QTY
    /// Task 577.
    /// </summary>
    public async Task<CostCorrectionScoreDto> CalculateCostCorrectionScoreDetailsAsync(Guid wsmRequestId, decimal? overrideTotalWsmCost = null)
    {
        var dto = new CostCorrectionScoreDto { WsmRequestId = wsmRequestId };
        decimal selfdep = await GetSelfDependencyScoreAsync(wsmRequestId);

        // 1. Fetch all category scores
        var tech = await CalculateHighCategoryTechnicalScoreDetailsAsync(wsmRequestId);
        var functional = await CalculateHighCategoryFunctionalScoreDetailsAsync(wsmRequestId);
        var user = await CalculateHighCategoryUserScoreDetailsAsync(wsmRequestId);
        var schedule = await CalculateHighCategoryScheduleScoreDetailsAsync(wsmRequestId);
        var cost = await CalculateHighCategoryCostScoreDetailsAsync(wsmRequestId);

        dto.TechnicalScore = tech.TotalHighCategoryTechnicalScore;
        dto.FunctionalScore = functional.TotalHighCategoryFunctionalScore;
        dto.UserScore = user.TotalHighCategoryUserScore;
        dto.ScheduleScore = schedule.TotalHighCategoryScheduleScore;
        dto.CostScore = cost.TotalHighCategoryCostScore;

        // 2. WSM_COMP_SCORE (The formula for correlation uses only the first 3 categories)
        dto.WsmCompositeScore = dto.TechnicalScore + dto.FunctionalScore + dto.ScheduleScore;

        // 3. Set TOTAL_WSM_COST
        if (overrideTotalWsmCost.HasValue)
        {
            dto.TotalWsmCost = overrideTotalWsmCost.Value;
        }
        else
        {
            // Fetch from database
            var costAssessment = await _context.WsmCostAssessments.AsNoTracking().FirstOrDefaultAsync(a => a.WsmRequestId == wsmRequestId);
            dto.TotalWsmCost = costAssessment?.TotalWsmCost ?? 0m;
        }

        // 4. COST_CORR_SCORE = (HICAT_COST_SCORE) / (TOTAL_WSM_COST)
        dto.CostCorrelatedScore = dto.TotalWsmCost > 0 ? Math.Round(dto.CostScore / dto.TotalWsmCost, 4) : 0m;

        return dto;
    }
    public async Task<DetailedSiaSummaryDto> CalculateDetailedSiaSummaryAsync(Guid wsmRequestId)
    {
        // 1. Get all active sections for Grand Max Weights
        var activeSections = await _context.DetailedSiaSections
            .Include(s => s.FunctionalArea)
            .Where(s => s.IsActive)
            .ToListAsync();

        var grandMaxCost = activeSections.Sum(s => s.MaxCostWeight);
        var grandMaxSchedule = activeSections.Sum(s => s.MaxScheduleWeight);
        var grandMaxPerformance = activeSections.Sum(s => s.MaxPerformanceWeight);

        // 2. Get current WSM responses
        var responses = await _context.WsmDetailedSiaResponses
            .Where(r => r.WsmRequestId == wsmRequestId)
            .ToListAsync();

        var responseDict = responses.ToDictionary(r => r.DetailedSiaSectionId);

        // 3. Group by Functional Area to calculate Area Summaries
        var areaGroups = activeSections.GroupBy(s => new { s.FunctionalAreaId, FunctionalAreaName = s.FunctionalArea?.Name });

        var areaSummaries = new List<DetailedSiaAreaSummaryDto>();
        foreach (var group in areaGroups)
        {
            var areaMaxCost = group.Sum(s => s.MaxCostWeight);
            var areaMaxSchedule = group.Sum(s => s.MaxScheduleWeight);
            var areaMaxPerformance = group.Sum(s => s.MaxPerformanceWeight);

            var areaActualCost = group
                .Where(s => responseDict.TryGetValue(s.Id, out var r) && r.IsImpacted)
                .Sum(s => s.MaxCostWeight);

            var areaActualSchedule = group
                .Where(s => responseDict.TryGetValue(s.Id, out var r) && r.IsImpacted)
                .Sum(s => s.MaxScheduleWeight);

            var areaActualPerformance = group
                .Where(s => responseDict.TryGetValue(s.Id, out var r) && r.IsImpacted)
                .Sum(s => s.MaxPerformanceWeight);

            areaSummaries.Add(new DetailedSiaAreaSummaryDto
            {
                FunctionalAreaId = group.Key.FunctionalAreaId,
                FunctionalAreaName = group.Key.FunctionalAreaName,
                MaxCostWeight = areaMaxCost,
                MaxScheduleWeight = areaMaxSchedule,
                MaxPerformanceWeight = areaMaxPerformance,
                ActualCostScore = areaActualCost,
                ActualScheduleScore = areaActualSchedule,
                ActualPerformanceScore = areaActualPerformance,
                CostPercentage = grandMaxCost > 0 ? Math.Round((areaActualCost / grandMaxCost) * 100, 2) : 0,
                SchedulePercentage = grandMaxSchedule > 0 ? Math.Round((areaActualSchedule / grandMaxSchedule) * 100, 2) : 0,
                PerformancePercentage = grandMaxPerformance > 0 ? Math.Round((areaActualPerformance / grandMaxPerformance) * 100, 2) : 0
            });
        }

        // 4. Grand Total Summary
        return new DetailedSiaSummaryDto
        {
            GrandMaxCostWeight = grandMaxCost,
            GrandMaxScheduleWeight = grandMaxSchedule,
            GrandMaxPerformanceWeight = grandMaxPerformance,
            TotalActualCostScore = areaSummaries.Sum(a => a.ActualCostScore),
            TotalActualScheduleScore = areaSummaries.Sum(a => a.ActualScheduleScore),
            TotalActualPerformanceScore = areaSummaries.Sum(a => a.ActualPerformanceScore),
            TotalWsmCostScore = Math.Round(areaSummaries.Sum(a => a.CostPercentage), 2),
            TotalWsmScheduleScore = Math.Round(areaSummaries.Sum(a => a.SchedulePercentage), 2),
            TotalWsmPerformanceScore = Math.Round(areaSummaries.Sum(a => a.PerformancePercentage), 2),
            AreaSummaries = areaSummaries
        };
    }
}