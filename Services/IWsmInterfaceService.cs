using PointScore.Models;

namespace PointScore.Services
{
    public interface IWsmInterfaceService
    {
        // Matrix and detailed assessment methods
        Task<IEnumerable<WsmInterfaceDetailResponseDto>> GetMatrixByBlockAsync(Guid blockId);
        Task<bool> SaveDetailedAssessmentAsync(Guid wsmId, Guid? targetWsmId, WsmInterfaceDetailDto dto, Guid? userId);
        Task<IEnumerable<WsmInterfaceDetailResponseDto>> GetDetailedAssessmentsAsync(Guid wsmId, Guid? targetWsmId);
        Task<InterdependencyScoreDto?> CalculateInterdependencyScoreAsync(Guid wsmId);
        Task<IEnumerable<WsmBlockInterfaceSummaryDto>> GetBlockInterfaceSummaryAsync(Guid blockId, bool? isInternal = null);
        Task<WsmGlobalBlockTotalDto> GetGlobalBlockTotalAsync(Guid blockId, bool? isInternal = null);
        
        // New Scoring Methods
        Task<WsmInterdependencyScoringDto> GetBlockInterdependencyScoringAsync(Guid blockId);
        Task<WsmComplexityScoringDto> GetBlockComplexityScoringAsync(Guid blockId);
    }

    public class WsmInterfaceDetailDto
    {

        public bool HasPhysical { get; set; }
        public int? PhysicalCount { get; set; }
        public bool HasEnergy { get; set; }
        public int? EnergyCount { get; set; }
        public bool HasMass { get; set; }
        public int? MassCount { get; set; }
        public bool HasInfo { get; set; }
        public int? InfoCount { get; set; }
        public bool IsInternal { get; set; }
        public string? Comments { get; set; }
    }

    public class InterdependencyScoreDto
    {
        public Guid WsmRequestId { get; set; }
        public string WsmRequestNumber { get; set; } = string.Empty;
        public int PhysicalScore { get; set; }
        public int EnergyScore { get; set; }
        public int MassScore { get; set; }
        public int InfoScore { get; set; }
        public int TotalScore { get; set; }
    }

    public class WsmInterfaceDetailResponseDto
    {
        public Guid Id { get; set; }
        public Guid WsmRequestId { get; set; }
        public string SourceWsmNumber { get; set; } = string.Empty;
        public Guid? TargetWsmId { get; set; }
        public string? TargetWsmNumber { get; set; }

        public bool HasPhysical { get; set; }
        public int? PhysicalCount { get; set; }
        public bool HasEnergy { get; set; }
        public int? EnergyCount { get; set; }
        public bool HasMass { get; set; }
        public int? MassCount { get; set; }
        public bool HasInfo { get; set; }
        public int? InfoCount { get; set; }
        public bool IsInternal { get; set; }
        public string? Comments { get; set; }
    }

    public class WsmBlockInterfaceSummaryDto
    {
        public Guid WsmRequestId { get; set; }
        public string WsmRequestNumber { get; set; } = string.Empty;
        public int TotalPhysical { get; set; }
        public int TotalEnergy { get; set; }
        public int TotalMass { get; set; }
        public int TotalInfo { get; set; }
        public int GrandTotal { get; set; }
    }

    public class WsmGlobalBlockTotalDto
    {
        public Guid BlockId { get; set; }
        public int GlobalPhysical { get; set; }
        public int GlobalEnergy { get; set; }
        public int GlobalMass { get; set; }
        public int GlobalInfo { get; set; }
        public int GlobalGrandTotal { get; set; }
    }

    public class WsmInterdependencyScoringDto
    {
        // Actual Totals (Row 115)
        public int PhysicalActual { get; set; }
        public int EnergyActual { get; set; }
        public int MassActual { get; set; }
        public int InfoActual { get; set; }
        public int GrandActual { get; set; }

        // Possible Totals (Row 118)
        public int PhysicalPossible { get; set; }
        public int EnergyPossible { get; set; }
        public int MassPossible { get; set; }
        public int InfoPossible { get; set; }
        public int GrandPossible { get; set; }

        // Weights (Row 119)
        public decimal PhysicalWeight { get; set; } = 0.20m;
        public decimal EnergyWeight { get; set; } = 0.30m;
        public decimal MassWeight { get; set; } = 0.30m;
        public decimal InfoWeight { get; set; } = 0.20m;
        public decimal GrandWeight { get; set; } = 1.00m;

        // Calculated Scores (Row 121)
        public decimal PhysicalScore { get; set; }
        public decimal EnergyScore { get; set; }
        public decimal MassScore { get; set; }
        public decimal InfoScore { get; set; }
        public decimal GrandTotalScore { get; set; }
    }

    public class WsmComplexityScoringDto
    {
        public int PhysicalCount { get; set; }
        public int EnergyCount { get; set; }
        public int MassCount { get; set; }
        public int InfoCount { get; set; }
        public int GrandTotalCount { get; set; }
        public decimal ScoreOutput { get; set; } // Range-based score [0.25 - 10]
    }
}
