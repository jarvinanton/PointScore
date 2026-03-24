using System;
using System.ComponentModel.DataAnnotations;
using PointScore.Models.Enums;


namespace PointScore.Models.DTOs
{
    public class WsmCreateDto
    {
        // === OBLIGATORIOS ===
        [Required] public string SystemName { get; set; } = null!;

        public string? WsmRequestNumber { get; set; } // ← Ahora es opcional, se genera automáticamente si no se proporciona
        [Required, StringLength(500)] public string Title { get; set; } = null!;
        [Required, StringLength(4000)] public string Description { get; set; } = null!;
        
        [Range(1, 10)] public float ProposedPriority { get; set; } = 5;

        public DateTime? DesiredNeedDate { get; set; }
        public DateTime? RequiredNeedDate { get; set; }

        public string? ImpactsBenefits { get; set; }
        public string? CommentsRecommendations { get; set; }
        public string? CommentsFollowOn { get; set; }

        // === ORIGINATOR ===
        [Required] public string OriginatorFullName { get; set; } = null!;
        public string? OriginatorTitle { get; set; }
        [Required, EmailAddress] public string OriginatorEmail { get; set; } = null!;
        public string Organization { get; set; } = "";
        public string? ContactNumber { get; set; }

        // === CHANGE DRIVERS ===
        public bool DriverWarfighter { get; set; }
        public bool DriverCyberRequirement { get; set; }
        public bool DriverECP { get; set; }
        public bool DriverInteroperability { get; set; }
        public bool DriverNewCapability { get; set; }
        public bool DriverObsolescence { get; set; }
        public bool DriverGFE { get; set; }
        public bool DriverRAM { get; set; }
        public bool DriverMaintenanceFix { get; set; }

        // === MODIFICATION TYPE ===
        [Required] 
        public string ModificationType { get; set; } = "Permanent"; // Permanent | Temporary | Maintenance
        public bool OtherCategory { get; set; }
        public string? OtherCategoryDescription { get; set; } // ← Nuevo campo libre
    }

   public class WsmDetailResponseDto
{
    public Guid Id { get; set; }
    public string RequestNumber { get; set; } = null!;
    public string SystemName { get; set; } = null!;
    public string Title { get; set; } = null!;
    public DateTime DateReceived { get; set; }
    public DateTime? DateSubmitted { get; set; }
    public float ProposedPriority { get; set; }  // Cambiado a int (1-10)
    public DateTime? DesiredNeedDate { get; set; }
    public DateTime? RequiredNeedDate { get; set; }
    public string Description { get; set; } = null!;
    public string? ImpactsBenefits { get; set; }
    public string? CommentsRecommendations { get; set; }
    public string? ImplementationFollowOnComments { get; set; }
    public float? OriScore { get; set; }  = null!;      // From WsmOriAssessment
    public float? TimeCriticalityScore { get; set; }   // From WsmTimeCriticalityAssessment
    public DateTime? AssignedDate { get; set; }

    // Personas
    public PersonDto? Originator { get; set; }
    public PersonDto? WsmOwner { get; set; }

    // Recommended Design Owner
    public RecommendedDesignOwnerInfoDto? RecommendedDesignOwner { get; set; }

    // NUEVO: Información del Block al que pertenece este WSM
    public BlockDto? Block { get; set; }

    // WBS Levels
    public List<WbsLevelDto> WbsLevels { get; set; } = new();

    // Impact Analysis
    public ImpactAnalysisDto? Impacts { get; set; }

    // Change Drivers
    public List<string> ChangeDrivers { get; set; } = new();

    // Modification Type
    public string ModificationType { get; set; } = null!;

    // Workflow Status
    public WorkflowStatusDto? Workflow { get; set; }

    // Other Category
    public bool OtherCategory { get; set; }
    public string? OtherCategoryDescription { get; set; }

    public List<FunctionalAreaReviewerDto> ImpactedFunctionalAreas { get; set; } = new();

    public DetailedSiaSummaryDto? DetailedSiaSummary { get; set; }

    public List<DetailedSiaResponseDto> DetailedSiaResponses { get; set; } = new();

    public List<WsmMrlResponseDto> MrlResponses { get; set; } = new();

    public TrlLevelDto? TrlLevel { get; set; }
}

public class WsmMrlResponseDto
{
    public int SubThreadId { get; set; }
    public string SubThreadCode { get; set; } = null!;
    public string SubThreadName { get; set; } = null!;
    public int Score { get; set; }
}

    public class PersonDto
    {
        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Title { get; set; }
        public string Email { get; set; } = null!;
        public string Organization { get; set; } = null!;
        public string? ContactNumber { get; set; }
    }

    public class RecommendedDesignOwnerInfoDto
    {
        public Guid Id { get; set; }
        public string OrganizationName { get; set; } = null!;
        public string ContactPerson { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public DateTime AssignmentDate { get; set; }
        public string Status { get; set; } = null!;  // "Recommended", "Approved", "Denied"
        public int StatusNumeric { get; set; }  // 0, 1, 2
        public DateTime? StatusChangedDate { get; set; }
        public string? StatusComments { get; set; }
    }

    public class WbsLevelDto
    {
        public string Level { get; set; } = null!;
        public string Nomenclature { get; set; } = null!;
        public bool IsApplicable { get; set; }
    }

    public class ImpactAnalysisDto
    {
        public bool Software { get; set; }
        public bool Cyber { get; set; }
        public bool Training { get; set; }
        public bool Safety { get; set; }
        public bool Requirements { get; set; }
        public bool ProductionLine { get; set; }
        public bool DevelopmentSIL { get; set; }
        public bool FieldTacticalSIL { get; set; }
        public bool TestEvent { get; set; }
        public bool DepotRepair { get; set; }
        public bool Other { get; set; }

        public bool SoftwareApplicability { get; set; }
        public bool Test { get; set; }
        public bool Quality { get; set; }
        public bool Logistics { get; set; }
        public bool SystemsEngineering { get; set; }
        public bool AcquisitionContracts { get; set; }
        public bool Finance { get; set; }
        public bool ProgramManagement { get; set; }
        public bool Security { get; set; }
    }

    public class WorkflowStatusDto
    {
        public string WorkflowStatus { get; set; } = null!;
        public bool DevCCB_Approved { get; set; }
        public DateTime? DevCCB_ApprovalDate { get; set; }
        public bool ProdCCB_Approved { get; set; }
        public DateTime? ProdCCB_ApprovalDate { get; set; }
        public bool ImplementationNumberAssigned { get; set; }
        public bool ImplementationCompleted { get; set; }
        public DateTime? ImplementationCompletedDate { get; set; }

        // Flight Test
        public bool? FlightTestRequired { get; set; }
        public DateTime? FlightTestDeterminationDate { get; set; }
        public string? FlightTestComments { get; set; }

        // Material Release
        public bool? MaterialReleaseRequired { get; set; }
        public DateTime? MaterialReleaseDeterminationDate { get; set; }
        public string? MaterialReleaseComments { get; set; }
        public string? MaterialReleaseDeterminedBy { get; set; }

        // Impact Threshold
        public string? ImpactThreshold { get; set; }
        public DateTime? ImpactThresholdDate { get; set; }
        public string? ImpactThresholdComments { get; set; }
    }

    public class WsmListItemDto
    {
        public Guid Id { get; set; }
        public string RequestNumber { get; set; } = null!;
        public string SystemName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTime DateReceived { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public float ProposedPriority { get; set; }
        public float OriScore { get; set; }
        public float? TimeCriticalityScore { get; set; }

        public string Status { get; set; } = null!;
        public string WsmOwnerName { get; set; } = null!;
        public string WsmOwnerId { get; set; } = null!;

        // NUEVO: Block information
        public string? BlockName { get; set; }
        public DateTime? BlockStartDate { get; set; }
        public DateTime? BlockEndDate { get; set; }
        public string? BlockOwnerName { get; set; }

        public bool IsAssigned { get; set; }
        public DateTime? DesiredNeedDate { get; set; }
        public DateTime? RequiredNeedDate { get; set; }
    }

    public class PaginatedResponse<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public IReadOnlyList<T> Items { get; set; } = null!;
    }

    public class WsmUpdateDto : WsmCreateDto
    {
        // Todos los campos son OPCIONALES en update
        // public new string? RequestNumber { get; set; } // No se permite cambiar
        public new string? SystemName { get; set; }
        public new string? Title { get; set; }
        public  DateTime? DateReceived { get; set; }
        public Guid? BlockId { get; set; }
        public  DateTime? DateSubmitted { get; set; }
        public new float ProposedPriority { get; set; }
        public new DateTime? DesiredNeedDate { get; set; }
        public new DateTime? RequiredNeedDate { get; set; }
        public new string? Description { get; set; }
        public new string? ImpactsBenefits { get; set; }
        public new string? CommentsRecommendations { get; set; }
        public new string? CommentsFollowOn { get; set; }
        public  DateTime? AssignedDate { get; set; }

        // Personas opcionales
        public new string? OriginatorFullName { get; set; }
        public new string? OriginatorTitle { get; set; }
        public new string? OriginatorEmail { get; set; }
        public new string? Organization { get; set; }
        public new string? ContactNumber { get; set; }

        public  string? WsmOwnerName { get; set; }
        public  string? WsmOwnerEmail { get; set; }
        public  string? BlockOwnerName { get; set; }
        public  string? BlockOwnerEmail { get; set; }

        // WBS opcionales
        public  string? WbsLevel1 { get; set; }
        public  string? WbsLevel2 { get; set; }
        public  string? WbsLevel3 { get; set; }
        public  string? WbsLevel4 { get; set; }
        public  string? WbsLevel5 { get; set; }
        public bool WbsLevel1Applicability { get; set; } = true;
        public bool WbsLevel2Applicability { get; set; } = true;
        public bool WbsLevel3Applicability { get; set; } = true;
        public bool WbsLevel4Applicability { get; set; } = true;
        public bool WbsLevel5Applicability { get; set; } = true;
        public new string ModificationType { get; set; } = "Permanent";

        // ====== IMPACT ANALYSIS (solo en Update, nunca en Create) ======
        public bool ImpactSoftware { get; set; }
        public bool ImpactCyber { get; set; }
        public bool ImpactTraining { get; set; }
        public bool ImpactSafety { get; set; }
        public bool ImpactRequirements { get; set; }
        public bool ImpactProductionLine { get; set; }
        public bool impactDevelopmentSil { get; set; }        // respeta el nombre exacto que tienes en la entidad
        public bool impactFieldTacticalSil { get; set; }      // igual
        public bool ImpactTestEvent { get; set; }
        public bool ImpactDepotRepair { get; set; }
        public bool ImpactOther { get; set; }

        public bool ImpactSoftwareApplicability { get; set; }
        public bool ImpactTest { get; set; }
        public bool ImpactQuality { get; set; }
        public bool ImpactLogistics { get; set; }
        public bool ImpactSystemsEngineering { get; set; }
        public bool ImpactAcquisitionContracts { get; set; }
        public bool ImpactFinance { get; set; }
        public bool ImpactProgramManagement { get; set; }
        public bool ImpactSecurity { get; set; }
        

    }
    public class WsmByBlockDto
    {
        public Guid Id { get; set; }
        public string RequestNumber { get; set; } = null!;
        public string SystemName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTime DateReceived { get; set; }
        public float ProposedPriority { get; set; }
        public float OriScore { get; set; }
        public string Status { get; set; } = null!;

        // Información del Block
        public string BlockName { get; set; } = null!;
        public DateTime? BlockStartDate { get; set; }
        public DateTime? BlockEndDate { get; set; }
        public string BlockOwnerName { get; set; } = null!;
        public string? BlockOwnerEmail { get; set; }

        // WSM Owner
        public string WsmOwnerName { get; set; } = null!;
        public string? WsmOwnerEmail { get; set; }

        public DateTime? AssignedDate { get; set; }
        public DateTime? DesiredNeedDate { get; set; }
        public DateTime? RequiredNeedDate { get; set; }
    }
    public class WsmDashboardDto
    {
        public Guid Id { get; set; }
        public string RequestNumber { get; set; } = null!;
        public string SystemName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTime DateReceived { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public float ProposedPriority { get; set; }
        public DateTime? DesiredNeedDate { get; set; }
        public DateTime? RequiredNeedDate { get; set; }
        public string Description { get; set; } = null!;
        public string? ImpactsBenefits { get; set; }
        public string? CommentsRecommendations { get; set; }
        public string? ImplementationFollowOnComments { get; set; }
        public float OriScore { get; set; }
        public DateTime? AssignedDate { get; set; }

        public string Status { get; set; } = null!;

        // WSM Owner
        public string WsmOwnerName { get; set; } = null!;
        public string? WsmOwnerEmail { get; set; }

        // NUEVO: Block information
        public string BlockName { get; set; } = null!;
        public DateTime? BlockStartDate { get; set; }
        public DateTime? BlockEndDate { get; set; }
        public string BlockOwnerName { get; set; } = null!;
        public string? BlockOwnerEmail { get; set; }

        // CCB Status
        public string DevCCB_Status { get; set; } = null!;
        public string ProdCCB_Status { get; set; } = null!;

        public bool IsClosed { get; set; }
        public bool ImplementationCompleted { get; set; }
    }
   public class WsmApprovedDto
    {
        public Guid Id { get; set; }
        public string RequestNumber { get; set; } = null!;
        public string SystemName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTime DateReceived { get; set; }
        public DateTime? DateSubmitted { get; set; }

        public string Status { get; set; } = null!;
        public DateTime? ProdCCB_ApprovalDate { get; set; }
        public string ImplementationNumber { get; set; } = null!;

        public string ModificationType { get; set; } = null!;
        public float OriScore { get; set; }
        public string WsmOwnerName { get; set; } = null!;
        public string? WsmOwnerEmail { get; set; }

        // NUEVO: Block information
        public string BlockName { get; set; } = null!;
        public DateTime? BlockStartDate { get; set; }
        public DateTime? BlockEndDate { get; set; }
        public string BlockOwnerName { get; set; } = null!;
        public string? BlockOwnerEmail { get; set; }

        public DateTime? DesiredNeedDate { get; set; }
        public DateTime? RequiredNeedDate { get; set; }
        public bool ImplementationCompleted { get; set; }
        public DateTime? ImplementationCompletedDate { get; set; }
    }
   public class WsmOpenDto
    {
        public Guid Id { get; set; }
        public string RequestNumber { get; set; } = null!;
        public string SystemName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTime DateReceived { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public float ProposedPriority { get; set; }

        public string Status { get; set; } = null!;

        public float OriScore { get; set; }

        // WSM Owner
        public string WsmOwnerName { get; set; } = null!;
        public string? WsmOwnerEmail { get; set; }

        // NUEVO: Block information
        public string BlockName { get; set; } = null!;
        public DateTime? BlockStartDate { get; set; }
        public DateTime? BlockEndDate { get; set; }
        public string BlockOwnerName { get; set; } = null!;
        public string? BlockOwnerEmail { get; set; }

        public DateTime? AssignedDate { get; set; }
        public DateTime? DesiredNeedDate { get; set; }
        public DateTime? RequiredNeedDate { get; set; }

        public string DevCCB_Status { get; set; } = null!;
        public string ProdCCB_Status { get; set; } = null!;
    }
    public class WsmClosedDto
    {
        public Guid Id { get; set; }
        public string RequestNumber { get; set; } = null!;
        public string SystemName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTime DateReceived { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public float ProposedPriority { get; set; }

        public string Status { get; set; } = null!;

        public float OriScore { get; set; }

        // WSM Owner
        public string WsmOwnerName { get; set; } = null!;
        public string? WsmOwnerEmail { get; set; }

        // NUEVO: Block information
        public string BlockName { get; set; } = null!;
        public DateTime? BlockStartDate { get; set; }
        public DateTime? BlockEndDate { get; set; }
        public string BlockOwnerName { get; set; } = null!;
        public string? BlockOwnerEmail { get; set; }

        public DateTime? AssignedDate { get; set; }
        public DateTime? DesiredNeedDate { get; set; }
        public DateTime? RequiredNeedDate { get; set; }
        public DateTime? ImplementationCompletedDate { get; set; }

        public string DevCCB_Status { get; set; } = null!;
        public string ProdCCB_Status { get; set; } = null!;
    }
    public class WsmSubmittedDto
    {
        public Guid Id { get; set; }
        public string RequestNumber { get; set; } = null!;
        public string SystemName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTime DateReceived { get; set; }
        public DateTime DateSubmitted { get; set; }

        public string OriginatorName { get; set; } = null!;
        public string? OriginatorEmail { get; set; }
        public string? OriginatorTitle { get; set; }
        public string? Organization { get; set; }  // ← Existe en WsmRequest

        public float ProposedPriority { get; set; }
        public float OriScore { get; set; }
        public bool HasSoftwareImpact { get; set; }
        public bool HasSafetyImpact { get; set; }
        public bool HasCyberImpact { get; set; }
        public bool HasProductionLineImpact { get; set; }

        public bool IsNewCapability { get; set; }
        public bool IsObsolescence { get; set; }
        public bool IsSafetyRelated { get; set; }
        public bool IsCyberRequirement { get; set; }

        public DateTime? DesiredNeedDate { get; set; }
        public DateTime? RequiredNeedDate { get; set; }
    }
    public class WsmErbDto
    {
        public Guid Id { get; set; }
        public string RequestNumber { get; set; } = null!;
        public string SystemName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTime? DateSubmitted { get; set; }

        public string Status { get; set; } = null!;
        public string MyRole { get; set; } = null!;

        public string WsmOwnerName { get; set; } = null!;
        public string BlockName { get; set; } = null!;
        public string BlockOwnerName { get; set; } = null!;

        public float ProposedPriority { get; set; }
        public float OriScore { get; set; }

        public DateTime? DesiredNeedDate { get; set; }
        public DateTime? RequiredNeedDate { get; set; }

        public string DevCCB_Status { get; set; } = null!;
        public string ProdCCB_Status { get; set; } = null!;
    }
    public class FunctionalAreaReviewerDto
    {
        public int FunctionalAreaId { get; set; }
        public string FunctionalAreaName { get; set; } = null!;
        public decimal Weight { get; set; }
        public Guid? ReviewerId { get; set; }
        public string ReviewerName { get; set; } = null!;
        public string? ReviewerEmail { get; set; }
        public string Status { get; set; } = null!;
        public DateTime AssignedDate { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string? Comments { get; set; }
    }
    public class WsmFullFormDto
    {
        // Página 1
        public string SystemName { get; set; } = null!;
        public string WsmRequestNumber { get; set; } = null!;
        public DateTime DateReceived { get; set; }
        public string OriginatorName { get; set; } = null!;
        public string? OriginatorEmail { get; set; }
        public string? OriginatorTitle { get; set; } = null!;
        public string OriginatorOrganization { get; set; } = null!;
        public string? ContactNumber { get; set; }
        public DateTime? DateWsmSubmitted { get; set; }

        public string TitleOfIssue { get; set; } = null!;
        public string ChangeIssueDescription { get; set; } = null!;
        public string? ImpactsBenefitsFromChange { get; set; }
        public string? CommentsAndRecommendations { get; set; }

        // WBS
        public string? WbsLevel1 { get; set; }
        public bool WbsLevel1Applicability { get; set; }
        public string? WbsLevel2 { get; set; }
        public bool WbsLevel2Applicability { get; set; }
        public string? WbsLevel3 { get; set; }
        public bool WbsLevel3Applicability { get; set; }
        public string? WbsLevel4 { get; set; }
        public bool WbsLevel4Applicability { get; set; }
        public string? WbsLevel5 { get; set; }
        public bool WbsLevel5Applicability { get; set; }

        // Impact Analysis
        public bool SoftwareImpact { get; set; }
        public bool Cyber { get; set; }
        public bool Training { get; set; }
        public bool Safety { get; set; }
        public bool Requirements { get; set; }
        public bool ProductionLine { get; set; }
        public bool DevSil { get; set; }
        public bool FieldTacticalSil { get; set; }
        public bool TestEvent { get; set; }
        public bool DepotRepair { get; set; }
        public bool OtherImpact { get; set; }

        // WSM Categories
        public bool Warfighter { get; set; }
        public bool CyberRequirementUpdate { get; set; }
        public bool EngineeringChangeProposalEcp { get; set; }
        public bool InteroperabilityRequirementUpdate { get; set; }
        public bool NewCapabilityRequirement { get; set; }
        public bool ObsolescenceUpdate { get; set; }
        public bool GfeUpdate { get; set; }
        public bool ReliabilityAndMaintainabilityRamRequirement { get; set; }
        public bool MaintenanceFix { get; set; }
        public bool OtherCategory { get; set; }
        public string? OtherCategoryDescription { get; set; }

        // Modification Type
        public bool PermanentModification { get; set; }
        public bool TemporaryModification { get; set; }
        public bool MaintenanceModification { get; set; }

        // ORI & Time Criticality
        public float OriScore { get; set; }
        public float TimeCriticalityScore { get; set; }

        // Página 3 & 4
        public string WsmAssignedOwner { get; set; } = null!;
        public DateTime? WsmAssignedOwnerDate { get; set; }
        public string AssignedBlockOwner { get; set; } = null!;

        public string WorkflowStatus { get; set; } = null!;

         public bool DevelopmentCcbApproved { get; set; }
        public DateTime? DevelopmentCcbApprovalDate { get; set; }
        public bool DevelopmentCcbDisapproved { get; set; }
        public DateTime? DevelopmentCcbDisapprovalDate { get; set; }

        public bool ProductionCcbApproved { get; set; }
        public DateTime? ProductionCcbApprovalDate { get; set; }
        public bool ProductionCcbDisapproved { get; set; }
        public DateTime? ProductionCcbDisapprovalDate { get; set; }

        public bool FieldingImplementationCcbApproved { get; set; }
        public DateTime? FieldingImplementationCcbApprovalDate { get; set; }

        public string? ImplementationNumber { get; set; }
        public bool ImplementationNumberAssigned { get; set; }
        public DateTime? ImplementationNumberAssignedDate { get; set; }
        public bool ImplementationCompleted { get; set; }
        public DateTime? ImplementationCompletedDate { get; set; }
        public string CommentsFollowOn { get; set; } = null!;
        public List<FunctionalAreaReviewerDto> ImpactedFunctionalAreas { get; set; } = new();
        public string BlockName { get; set; } = null!;
        public DateTime? BlockStartDate { get; set; }
        public DateTime? BlockEndDate { get; set; }
        public string BlockOwnerName { get; set; } = null!;

        // Flight Test
        public bool? FlightTestRequired { get; set; }
        public DateTime? FlightTestDeterminationDate { get; set; }
        public string? FlightTestComments { get; set; }

        // Material Release
        public bool? MaterialReleaseRequired { get; set; }
        public DateTime? MaterialReleaseDeterminationDate { get; set; }
        public string? MaterialReleaseComments { get; set; }
        public string? MaterialReleaseDeterminedBy { get; set; }

        // Impact Threshold
        public string? ImpactThreshold { get; set; }
        public DateTime? ImpactThresholdDate { get; set; }
        public string? ImpactThresholdComments { get; set; }
    }
        public class WsmCategoryDashboardDto
    {
        public int TotalWsms { get; set; }

        // Categories ahora son dinámicas
        public Dictionary<string, int> Categories { get; set; } = new();

        // Modification Types también dinámicos
        public Dictionary<string, int> ModificationTypes { get; set; } = new();

        // Workflow Status (queda igual)
        public int PendingAssignment { get; set; }
        public int InProgress { get; set; }
        public int Approved { get; set; }
        public int Closed { get; set; }
    }
    public class InitiateErbFlowRequestDto
    {
        public bool ApproveWsm { get; set; } 
    }
    public class DesignCcbApprovalRequestDto
    {
        public bool DesignCcbApproved { get; set; }

        public string? CommentsFollowOn { get; set; }

    }
    public class ProductionCcbApprovalRequestDto
    {
        public bool ProductionCcbApproved { get; set; } // True = approve, False = reject
        public string? CommentsFollowOn { get; set; }   // Obligatorio si se rechaza
    }
    public class GovtApprovalSubmissionDto
    {
        [Required(ErrorMessage = "Design owner information is required")]
        public RecommendedDesignOwnerDto RecommendedDesignOwner { get; set; } = null!;

        [MaxLength(100)]
        public string? ImplementationNumber { get; set; }
    }

    public class RecommendedDesignOwnerDto
    {
        [Required]
        [MaxLength(200)]
        public string OrganizationName { get; set; } = null!;
        
        [Required]
        [MaxLength(100)]
        public string ContactPerson { get; set; } = null!;
        
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string ContactEmail { get; set; } = null!;
    }

    public class AssignRecommendedDesignOwnerDto
    {
        [Required]
        public Guid WsmId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string OrganizationName { get; set; } = null!;
        
        [Required]
        [MaxLength(100)]
        public string ContactPerson { get; set; } = null!;
        
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string ContactEmail { get; set; } = null!;
    }

    public class UpdateDesignOwnerStatusDto
    {
        [Required]
        public string Status { get; set; } = null!; // "Recommended", "Approved", "Denied"
        
        [MaxLength(1000)]
        public string? Comments { get; set; }
    }

    public class AssignImpactedAreasDto
    {
        [Required] public List<int> ImpactedAreaIds { get; set; } = new();
    }

    public class FlightTestThresholdDto
    {
        [Required] public bool FlightTestRequired { get; set; }  // true = Yes, false = No

        public string? Comments { get; set; } // Obligatorio si cambia la determinación inicial
    }

    public class MaterialReleaseDeterminationDto
    {
        [Required] public bool MaterialReleaseRequired { get; set; }  // true = Yes, false = No

        [Required] public string DeterminedBy { get; set; } = null!;  // "Safety", "Logistics", "Both"

        public string? Comments { get; set; } // Obligatorio si MaterialReleaseRequired = false
    }

    public class ImpactThresholdDto
    {
        [Required] public string ImpactThreshold { get; set; } = null!; // High, Medium, Low, None
        public string? Comments { get; set; } // Mandatory only when “High” is selected
    }

    public class InitialSiaAssessmentDto
    {
        [Required] public List<int> ImpactedAreaIds { get; set; } = new();
    }
    public class ErbReviewerAssignmentDto
    {
        [Required] public int FunctionalAreaId { get; set; }
        [Required] public Guid ReviewerId { get; set; }
    }

    public class AssignErbReviewersDto
    {
        [Required] public List<ErbReviewerAssignmentDto> Assignments { get; set; } = new();
    }

    public class UpdateFunctionalAreaDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required, Range(0.01, 1.0)]
        public decimal Weight { get; set; }  // 0.01 to 1.00 (1% to 100%)

        [Required]
        public bool IsActive { get; set; } = true;
    }
    public class FunctionalAreaDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Weight { get; set; }  // 0.10, 0.05, etc.
        public decimal WeightPercentage { get; set; }  // 10, 5, etc. (más amigable para frontend)
        public string? DefaultRoleName { get; set; }
    }
    public class DetailedSiaResponseDto
    {
        [Required] public int SectionId { get; set; }
        public string? SectionName { get; set; }
        public int? FunctionalAreaId { get; set; }
        public bool? IsImpacted { get; set; }
        public string? Comments { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // Max Weights (Master Data)
        public decimal? MaxCostWeight { get; set; }
        public decimal? MaxScheduleWeight { get; set; }
        public decimal? MaxPerformanceWeight { get; set; }

        // Functional Scoring (Calculated: IsImpacted ? MaxWeight : 0)
        public decimal? CostFunctionalScoring { get; set; }
        public decimal? ScheduleFunctionalScoring { get; set; }
        public decimal? PerformanceFunctionalScoring { get; set; }
    }

    public class DetailedSiaAreaSummaryDto
    {
        public int? FunctionalAreaId { get; set; }
        public string? FunctionalAreaName { get; set; }
        public decimal MaxCostWeight { get; set; }
        public decimal MaxScheduleWeight { get; set; }
        public decimal MaxPerformanceWeight { get; set; }
        public decimal ActualCostScore { get; set; }
        public decimal ActualScheduleScore { get; set; }
        public decimal ActualPerformanceScore { get; set; }
        public decimal CostPercentage { get; set; }
        public decimal SchedulePercentage { get; set; }
        public decimal PerformancePercentage { get; set; }
    }

    public class DetailedSiaSummaryDto
    {
        public decimal GrandMaxCostWeight { get; set; }
        public decimal GrandMaxScheduleWeight { get; set; }
        public decimal GrandMaxPerformanceWeight { get; set; }
        public decimal TotalActualCostScore { get; set; }
        public decimal TotalActualScheduleScore { get; set; }
        public decimal TotalActualPerformanceScore { get; set; }
        public decimal TotalWsmCostScore { get; set; }
        public decimal TotalWsmScheduleScore { get; set; }
        public decimal TotalWsmPerformanceScore { get; set; }
        public List<DetailedSiaAreaSummaryDto> AreaSummaries { get; set; } = new();
    }

    public class DetailedSiaSectionDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public object FunctionalArea { get; set; } = null!;
        public int Order { get; set; }
        public decimal MaxCostWeight { get; set; }
        public decimal MaxScheduleWeight { get; set; }
        public decimal MaxPerformanceWeight { get; set; }
    }

    public class UpdateDetailedSiaSectionDto
    {
        [Required, MaxLength(200)]
        public string Title { get; set; } = null!;

        [Required]
        public int Order { get; set; }

        public bool IsActive { get; set; } = true;

        public decimal MaxCostWeight { get; set; }
        public decimal MaxScheduleWeight { get; set; }
        public decimal MaxPerformanceWeight { get; set; }
    }

    public class UpdateDetailedSiaSectionWithIdDto : UpdateDetailedSiaSectionDto
    {
        [Required]
        public int Id { get; set; }
    }

    public class BulkUpdateDetailedSiaSectionsDto
    {
        [Required]
        public List<UpdateDetailedSiaSectionWithIdDto> Sections { get; set; } = new();
    }

    // === WSM COST ASSESSMENT DTOs ===
    
    public class WsmCostAssessmentInputDto
    {
        [Range(0, double.MaxValue, ErrorMessage = "Development cost must be non-negative")]
        public decimal DevelopmentCost { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Production cost per unit must be non-negative")]
        public decimal ProductionCostPerUnit { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Production quantity must be non-negative")]
        public int ProductionQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Installation cost must be non-negative")]
        public decimal InstallationCost { get; set; }
    }

    public class WsmCostAssessmentResultDto
    {
        public int Id { get; set; }
        public Guid WsmRequestId { get; set; }
        public decimal DevelopmentCost { get; set; }
        public decimal ProductionCostPerUnit { get; set; }
        public int ProductionQuantity { get; set; }
        public decimal TotalProductionCost { get; set; }
        public decimal InstallationCost { get; set; }
        public decimal CostScore { get; set; }
        public decimal TotalWsmCost { get; set; }
        public decimal CostCorrelatedScore { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
        


    // === SCORING FORMULA WEIGHTS DTOs ===
    
    public class ScoringWeightsDto
    {
        public int Id { get; set; }
        public decimal TechnicalWeight { get; set; }
        public decimal FunctionalWeight { get; set; }
        public decimal ScheduleWeight { get; set; }
        public decimal UserImpactWeight { get; set; }
        public decimal CostWeight { get; set; }
        public bool IsActive { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? UpdatedByName { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? Comments { get; set; }
    }

    public class ScoringWeightsUpdateDto
    {
        [Required]
        [Range(0, 100, ErrorMessage = "Technical weight must be between 0 and 100")]
        public decimal TechnicalWeight { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Functional weight must be between 0 and 100")]
        public decimal FunctionalWeight { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Schedule weight must be between 0 and 100")]
        public decimal ScheduleWeight { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "User impact weight must be between 0 and 100")]
        public decimal UserImpactWeight { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Cost weight must be between 0 and 100")]
        public decimal CostWeight { get; set; }

        [MaxLength(500)]
        public string? Comments { get; set; }
    }

    public class WsmIptMemberDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FunctionalArea { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime AssignedDate { get; set; }
    }
}
