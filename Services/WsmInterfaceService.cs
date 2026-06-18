using PointScore.Data;
using PointScore.Models;
using Microsoft.EntityFrameworkCore;

namespace PointScore.Services
{
    public class WsmInterfaceService : IWsmInterfaceService
    {
        private readonly CoreDbContext _context;

        public WsmInterfaceService(CoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WsmInterfaceDetailResponseDto>> GetMatrixByBlockAsync(Guid blockId)
        {
            // Get all WSMs in this block
            var wsmIds = await _context.WsmRequests
                .Where(w => w.BlockId == blockId)
                .Select(w => w.Id)
                .ToListAsync();

            // Get all detailed interface assessments where both source and target are in this block
            var assessments = await _context.WsmInterfaceDetails
                .Include(d => d.WsmRequest)
                .Include(d => d.TargetWsm)
                .Where(d => wsmIds.Contains(d.WsmRequestId) &&
                            (d.TargetWsmId == null || wsmIds.Contains(d.TargetWsmId.Value)))
                .OrderBy(d => d.WsmRequestId)
                .ThenBy(d => d.TargetWsmId)
                .Select(d => new WsmInterfaceDetailResponseDto
                {
                    Id = d.Id,
                    WsmRequestId = d.WsmRequestId,
                    SourceWsmNumber = d.WsmRequest != null ? d.WsmRequest.RequestNumber : "",
                    TargetWsmId = d.TargetWsmId,
                    TargetWsmNumber = d.TargetWsm != null ? d.TargetWsm.RequestNumber : null,

                    HasPhysical = d.HasPhysical,
                    HasEnergy = d.HasEnergy,
                    HasMass = d.HasMass,
                    HasInfo = d.HasInfo,
                    IsInternal = d.IsInternal,
                    Comments = d.Comments
                })
                .ToListAsync();

            return assessments;
        }

        public async Task<bool> SaveDetailedAssessmentAsync(Guid wsmId, Guid? targetWsmId, WsmInterfaceDetailDto dto, Guid? userId)
        {
            // Validate source WSM exists
            var wsmExists = await _context.WsmRequests.AnyAsync(w => w.Id == wsmId);
            if (!wsmExists) return false;

            // If TargetWsmId is provided, validate it exists
            if (targetWsmId.HasValue)
            {
                var targetExists = await _context.WsmRequests.AnyAsync(w => w.Id == targetWsmId.Value);
                if (!targetExists) return false;
            }

            // Check if this specific row already exists (upsert)
            var existing = await _context.WsmInterfaceDetails
                .FirstOrDefaultAsync(d =>
                    d.WsmRequestId == wsmId &&
                    d.TargetWsmId == targetWsmId &&
                    d.IsInternal == dto.IsInternal);

            if (existing != null)
            {
                // Update existing
                existing.HasPhysical = dto.HasPhysical;
                existing.PhysicalCount = dto.PhysicalCount;
                existing.HasEnergy = dto.HasEnergy;
                existing.EnergyCount = dto.EnergyCount;
                existing.HasMass = dto.HasMass;
                existing.MassCount = dto.MassCount;
                existing.HasInfo = dto.HasInfo;
                existing.InfoCount = dto.InfoCount;
                existing.Comments = dto.Comments;
                existing.ModifiedAt = DateTime.UtcNow;
            }
            else
            {
                // Create new
                var newDetail = new WsmInterfaceDetail
                {
                    WsmRequestId = wsmId,
                    TargetWsmId = targetWsmId,

                    HasPhysical = dto.HasPhysical,
                    PhysicalCount = dto.PhysicalCount,
                    HasEnergy = dto.HasEnergy,
                    EnergyCount = dto.EnergyCount,
                    HasMass = dto.HasMass,
                    MassCount = dto.MassCount,
                    HasInfo = dto.HasInfo,
                    InfoCount = dto.InfoCount,
                    IsInternal = dto.IsInternal,
                    Comments = dto.Comments,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow
                };
                _context.WsmInterfaceDetails.Add(newDetail);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<WsmInterfaceDetailResponseDto>> GetDetailedAssessmentsAsync(Guid wsmId, Guid? targetWsmId)
        {
            var query = _context.WsmInterfaceDetails
                .Include(d => d.WsmRequest)
                .Include(d => d.TargetWsm)
                .Where(d => d.WsmRequestId == wsmId);

            if (targetWsmId.HasValue)
            {
                query = query.Where(d => d.TargetWsmId == targetWsmId.Value);
            }

            return await query
                .Select(d => new WsmInterfaceDetailResponseDto
                {
                    Id = d.Id,
                    WsmRequestId = d.WsmRequestId,
                    SourceWsmNumber = d.WsmRequest != null ? d.WsmRequest.RequestNumber : "",
                    TargetWsmId = d.TargetWsmId,
                    TargetWsmNumber = d.TargetWsm != null ? d.TargetWsm.RequestNumber : null,

                    HasPhysical = d.HasPhysical,
                    HasEnergy = d.HasEnergy,
                    HasMass = d.HasMass,
                    HasInfo = d.HasInfo,
                    IsInternal = d.IsInternal,
                    Comments = d.Comments
                })
                .ToListAsync();
        }

        public async Task<InterdependencyScoreDto?> CalculateInterdependencyScoreAsync(Guid wsmId)
        {
            var wsm = await _context.WsmRequests.FindAsync(wsmId);
            if (wsm == null) return null;

            // Get all detailed assessments where this WSM is the target (column in matrix)
            // AND internal assessments for this WSM
            var assessments = await _context.WsmInterfaceDetails
                .Where(d => d.TargetWsmId == wsmId || (d.WsmRequestId == wsmId && d.IsInternal))
                .ToListAsync();

            // Simplified logic: Just count True values regardless of row number
            var physicalScore = assessments.Count(d => d.HasPhysical);
            var energyScore = assessments.Count(d => d.HasEnergy);
            var massScore = assessments.Count(d => d.HasMass);
            var infoScore = assessments.Count(d => d.HasInfo);

            return new InterdependencyScoreDto
            {
                WsmRequestId = wsmId,
                WsmRequestNumber = wsm.RequestNumber,
                PhysicalScore = physicalScore,
                EnergyScore = energyScore,
                MassScore = massScore,
                InfoScore = infoScore,
                TotalScore = physicalScore + energyScore + massScore + infoScore
            };
        }

        public async Task<IEnumerable<WsmBlockInterfaceSummaryDto>> GetBlockInterfaceSummaryAsync(Guid blockId, bool? isInternal = null)
        {
            // Get all WSMs in this block
            var wsms = await _context.WsmRequests
                .Where(w => w.BlockId == blockId)
                .Select(w => new { w.Id, w.RequestNumber })
                .ToListAsync();

            var wsmIds = wsms.Select(w => w.Id).ToList();

            // Get all relevant assessments for this block (Target in block OR Internal of WSM in block)
            var query = _context.WsmInterfaceDetails.AsQueryable();

            if (isInternal.HasValue)
            {
                if (isInternal.Value)
                {
                    // Internal only: Source in block AND isInternal = true
                    query = query.Where(d => wsmIds.Contains(d.WsmRequestId) && d.IsInternal);
                }
                else
                {
                    // External only: Target in block OR (source in block with TargetWsmId=null), AND isInternal = false
                    query = query.Where(d => !d.IsInternal && (
                        (d.TargetWsmId != null && wsmIds.Contains(d.TargetWsmId.Value)) ||
                        (d.TargetWsmId == null && wsmIds.Contains(d.WsmRequestId))
                    ));
                }
            }
            else
            {
                // Both: Existing logic with TargetWsmId=null support
                query = query.Where(d =>
                    (d.TargetWsmId != null && wsmIds.Contains(d.TargetWsmId.Value)) ||
                    (d.TargetWsmId == null && wsmIds.Contains(d.WsmRequestId)) ||
                    (wsmIds.Contains(d.WsmRequestId) && d.IsInternal));
            }

            var allAssessments = await query.ToListAsync();

            var summaries = new List<WsmBlockInterfaceSummaryDto>();

            foreach (var w in wsms)
            {
                // Filter for this specific WSM (Incoming + Internal based on query above)
                var wsmAssessments = allAssessments
                    .Where(d => (!isInternal.HasValue &&
                        (d.TargetWsmId == w.Id || (d.WsmRequestId == w.Id && d.TargetWsmId == null) || (d.WsmRequestId == w.Id && d.IsInternal))) ||
                        (isInternal.HasValue && isInternal.Value && d.WsmRequestId == w.Id) ||
                        (isInternal.HasValue && !isInternal.Value && (d.TargetWsmId == w.Id || (d.WsmRequestId == w.Id && d.TargetWsmId == null))))
                    .ToList();

                var summary = new WsmBlockInterfaceSummaryDto
                {
                    WsmRequestId = w.Id,
                    WsmRequestNumber = w.RequestNumber,
                    TotalPhysical = wsmAssessments.Sum(d => d.PhysicalCount ?? (d.HasPhysical ? 1 : 0)),
                    TotalEnergy = wsmAssessments.Sum(d => d.EnergyCount ?? (d.HasEnergy ? 1 : 0)),
                    TotalMass = wsmAssessments.Sum(d => d.MassCount ?? (d.HasMass ? 1 : 0)),
                    TotalInfo = wsmAssessments.Sum(d => d.InfoCount ?? (d.HasInfo ? 1 : 0))
                };
                summary.GrandTotal = summary.TotalPhysical + summary.TotalEnergy + summary.TotalMass + summary.TotalInfo;
                summaries.Add(summary);
            }

            return summaries.OrderBy(s => s.WsmRequestNumber);
        }

        public async Task<WsmGlobalBlockTotalDto> GetGlobalBlockTotalAsync(Guid blockId, bool? isInternal = null)
        {
            // Reuse the existing summary logic to get totals per WSM
            var wsmSummaries = await GetBlockInterfaceSummaryAsync(blockId, isInternal);

            // Aggregate all WSM totals
            var globalTotal = new WsmGlobalBlockTotalDto
            {
                BlockId = blockId,
                GlobalPhysical = wsmSummaries.Sum(s => s.TotalPhysical),
                GlobalEnergy = wsmSummaries.Sum(s => s.TotalEnergy),
                GlobalMass = wsmSummaries.Sum(s => s.TotalMass),
                GlobalInfo = wsmSummaries.Sum(s => s.TotalInfo)
            };
            
            globalTotal.GlobalGrandTotal = globalTotal.GlobalPhysical + globalTotal.GlobalEnergy +
                                           globalTotal.GlobalMass + globalTotal.GlobalInfo;

            return globalTotal;
        }
        public async Task<WsmInterdependencyScoringDto> GetBlockInterdependencyScoringAsync(Guid blockId)
        {
            var numWsms = await _context.WsmRequests.CountAsync(w => w.BlockId == blockId);
            var totals = await GetGlobalBlockTotalAsync(blockId, false); // IsInternal = false

            var result = new WsmInterdependencyScoringDto
            {
                PhysicalActual = totals.GlobalPhysical,
                EnergyActual = totals.GlobalEnergy,
                MassActual = totals.GlobalMass,
                InfoActual = totals.GlobalInfo,
                GrandActual = totals.GlobalGrandTotal,

                PhysicalPossible = numWsms,
                EnergyPossible = numWsms,
                MassPossible = numWsms,
                InfoPossible = numWsms,
                GrandPossible = numWsms * 4
            };

            if (numWsms > 0)
            {
                result.PhysicalScore = (decimal)result.PhysicalActual / numWsms * result.PhysicalWeight;
                result.EnergyScore = (decimal)result.EnergyActual / numWsms * result.EnergyWeight;
                result.MassScore = (decimal)result.MassActual / numWsms * result.MassWeight;
                result.InfoScore = (decimal)result.InfoActual / numWsms * result.InfoWeight;
                
                // Total Score calculation as (GrandActual / GrandPossible)
                result.GrandTotalScore = Math.Round((decimal)result.GrandActual / result.GrandPossible, 2);
            }

            return result;
        }

        public async Task<WsmComplexityScoringDto> GetBlockComplexityScoringAsync(Guid blockId)
        {
            var totals = await GetGlobalBlockTotalAsync(blockId, true); // IsInternal = true

            var result = new WsmComplexityScoringDto
            {
                PhysicalCount = totals.GlobalPhysical,
                EnergyCount = totals.GlobalEnergy,
                MassCount = totals.GlobalMass,
                InfoCount = totals.GlobalInfo,
                GrandTotalCount = totals.GlobalGrandTotal,
                ScoreOutput = CalculateComplexityRangeScore(totals.GlobalGrandTotal)
            };

            return result;
        }

        private decimal CalculateComplexityRangeScore(int total)
        {
            if (total <= 25) return 0.25m;
            if (total <= 50) return 0.51m;
            if (total <= 75) return 0.73m;
            if (total <= 100) return 1.00m;
            if (total <= 125) return 1.28m;
            if (total <= 150) return 1.53m;
            if (total <= 200) return 1.85m;
            if (total <= 250) return 2.68m;
            if (total <= 500) return 4.91m;
            return 10.00m;
        }
    }
}
