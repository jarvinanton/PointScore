namespace PointScore.Models.DTOs
{
    // ============================
    // WSM Report DTOs
    // ============================

    /// <summary>
    /// General summary statistics for the WSM Reports dashboard
    /// </summary>
    public class WsmReportSummaryDto
    {
        public int TotalWsms { get; set; }
        public int PendingCount { get; set; }
        public int InProgressCount { get; set; }
        public int DevCcbApprovedCount { get; set; }
        public int DevCcbRejectedCount { get; set; }
        public int ProdCcbApprovedCount { get; set; }
        public int ApprovedCount { get; set; }
        public int ClosedCount { get; set; }
        public float AverageOriScore { get; set; }
        public float AverageTimeCriticalityScore { get; set; }
        public float AveragePriority { get; set; }
        public int AssignedToBlockCount { get; set; }
        public int UnassignedToBlockCount { get; set; }
    }

    /// <summary>
    /// WSM count grouped by workflow status
    /// </summary>
    public class WsmByStatusReportDto
    {
        public string Status { get; set; } = null!;
        public int Count { get; set; }
        public decimal Percentage { get; set; }
        public List<WsmReportItemDto> Wsms { get; set; } = new();
    }

    /// <summary>
    /// WSM count grouped by functional area impact
    /// </summary>
    public class WsmByFunctionalAreaReportDto
    {
        public int FunctionalAreaId { get; set; }
        public string FunctionalAreaName { get; set; } = null!;
        public decimal Weight { get; set; }
        public int TotalImpactedWsms { get; set; }
        public int ReviewedCount { get; set; }
        public int PendingReviewCount { get; set; }
        public decimal Percentage { get; set; }
        public List<WsmReportItemDto> Wsms { get; set; } = new();
    }

    /// <summary>
    /// WSM count grouped by IPT member (reviewer)
    /// </summary>
    public class WsmByIptMemberReportDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int TotalAssignedWsms { get; set; }
        public int ReviewedCount { get; set; }
        public int PendingReviewCount { get; set; }
        public List<IptMemberWsmDetailDto> Wsms { get; set; } = new();
    }

    /// <summary>
    /// Detail of a WSM for IPT member reports
    /// </summary>
    public class IptMemberWsmDetailDto
    {
        public Guid WsmId { get; set; }
        public string RequestNumber { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string FunctionalAreaName { get; set; } = null!;
        public string ReviewStatus { get; set; } = null!;
        public DateTime AssignedDate { get; set; }
        public DateTime? ReviewedDate { get; set; }
    }

    /// <summary>
    /// Lightweight WSM item for reports
    /// </summary>
    public class WsmReportItemDto
    {
        public Guid Id { get; set; }
        public string RequestNumber { get; set; } = null!;
        public string SystemName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Status { get; set; } = null!;
        public float ProposedPriority { get; set; }
        public float OriScore { get; set; }
        public string WsmOwnerName { get; set; } = "Not Assigned";
        public string? BlockName { get; set; }
        public string ModificationType { get; set; } = null!;
        public DateTime DateReceived { get; set; }
        public DateTime? AssignedDate { get; set; }
    }

    /// <summary>
    /// WSM count grouped by modification type
    /// </summary>
    public class WsmByModificationTypeReportDto
    {
        public int ModificationTypeId { get; set; }
        public string ModificationTypeName { get; set; } = null!;
        public int Count { get; set; }
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// WSM count grouped by Block
    /// </summary>
    public class WsmByBlockReportDto
    {
        public Guid BlockId { get; set; }
        public string BlockName { get; set; } = null!;
        public string BlockOwnerName { get; set; } = "Not Assigned";
        public int TotalWsms { get; set; }
        public int? MaxPointCapacity { get; set; }
        public int? MaxCost { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<WsmReportItemDto> Wsms { get; set; } = new();
    }

    /// <summary>
    /// WSM count grouped by Change Driver type
    /// </summary>
    public class WsmByChangeDriverReportDto
    {
        public int ChangeDriverTypeId { get; set; }
        public string ChangeDriverName { get; set; } = null!;
        public int Count { get; set; }
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// Time-based trend report (WSMs created per period)
    /// </summary>
    public class WsmTrendReportDto
    {
        public string Period { get; set; } = null!; // "2026-01", "2026-02", etc.
        public int CreatedCount { get; set; }
        public int ClosedCount { get; set; }
        public int ApprovedCount { get; set; }
    }

    /// <summary>
    /// Complete report response with all statistics
    /// </summary>
    public class WsmFullReportDto
    {
        public WsmReportSummaryDto Summary { get; set; } = null!;
        public List<WsmByStatusReportDto> ByStatus { get; set; } = new();
        public List<WsmByFunctionalAreaReportDto> ByFunctionalArea { get; set; } = new();
        public List<WsmByIptMemberReportDto> ByIptMember { get; set; } = new();
        public List<WsmByModificationTypeReportDto> ByModificationType { get; set; } = new();
        public List<WsmByBlockReportDto> ByBlock { get; set; } = new();
        public List<WsmByChangeDriverReportDto> ByChangeDriver { get; set; } = new();
        public List<WsmTrendReportDto> MonthlyTrend { get; set; } = new();
    }

    /// <summary>
    /// Filter parameters for report endpoints
    /// </summary>
    public class WsmReportFilterDto
    {
        public string? Status { get; set; }
        public string? SystemName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public Guid? BlockId { get; set; }
        public Guid? WsmOwnerId { get; set; }
        public int? ModificationTypeId { get; set; }
    }
}