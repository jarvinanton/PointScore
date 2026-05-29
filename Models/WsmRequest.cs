// Models/Wsm.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PointScore.Models;
using PointScore.Models.Enums;

namespace PointScore.Models
{
   [Table("WsmRequests")]
    public class WsmRequest
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(20)]
        public string RequestNumber { get; set; } = null!;

        [Required, MaxLength(100)]
        public string SystemName { get; set; } = null!;

        public DateTime DateReceived { get; set; }
        public DateTime DateSubmitted { get; set; }

        [Required, MaxLength(500)]
        public string Title { get; set; } = null!;

        [Range(1, 10)]
        public float ProposedPriority { get; set; }

        public DateTime DesiredNeedDate { get; set; }
        public DateTime RequiredNeedDate { get; set; }

        [Required]
        public string Description { get; set; } = null!;

        public string? ImpactsBenefits { get; set; }
        public string? CommentsRecommendations { get; set; }
        public string? ImplementationFollowOnComments { get; set; }
        public Guid? RecommendedDesignOwnerId { get; set; }
        public virtual RecommendedDesignOwner? RecommendedDesignOwner { get; set; }    
        public string? ImplementationNumber { get; set; }

        public DateTime? AssignedDate { get; set; }

        // FKs
        public Guid OriginatorInfoId { get; set; }
        public Guid? WsmOwnerId { get; set; }
        public Guid? WsmOwnerUserId { get; set; }
        public int ModificationTypeId { get; set; }

        // Navegación
        public OriginatorInfo Originator { get; set; } = null!;
        public WsmOwner? WsmOwner { get; set; }
        public ModificationType ModificationType { get; set; } = null!;
        public ApprovalWorkflow ApprovalWorkflow { get; set; } = null!;
        public ImpactAnalysis? ImpactAnalysis { get; set; }

		// En WsmRequest.cs
		public ICollection<WsmWbsStructure> WbsStructures { get; set; } = new List<WsmWbsStructure>();
		public ICollection<WsmChangeDriver> ChangeDrivers { get; set; } = new List<WsmChangeDriver>();
		public ICollection<WsmComment> Comments { get; set; } = new List<WsmComment>();
        public ICollection<WsmOtherDetail> OtherDetails { get; set; } = new List<WsmOtherDetail>();
        public ICollection<WsmFunctionalImpact> FunctionalImpacts { get; set; } = new List<WsmFunctionalImpact>();
        public ICollection<WsmDeliverable> Deliverables { get; set; } = new List<WsmDeliverable>();
        public ICollection<WsmDetailedSiaResponse> DetailedSiaResponses { get; set; } = new List<WsmDetailedSiaResponse>();

        public Guid? BlockId { get; set; }
        public Block? Block { get; set; }
        public WsmSiaScore? SiaScore { get; set; }
        public virtual FeatureScoreResult? FeatureScoreResult { get; set; }
        public virtual WsmOriAssessment? OriAssessment { get; set; }
        public virtual WsmTimeCriticalityAssessment? TimeCriticalityAssessment { get; set; }
        public virtual WsmWeight? WsmWeight { get; set; }

        // WSM Owner – también directo con User
        public User? WsmOwnerUser { get; set; }
        
        // public ICollection<WsmBlockAssignment> BlockAssignments { get; set; } = new List<WsmBlockAssignment>();

        public virtual TRL_level? TrlLevel { get; set; }
        public virtual ICollection<WsmMilestoneResponse> MilestoneResponses { get; set; } = new List<WsmMilestoneResponse>();
        public decimal? MrlCompositeScore { get; set; }
        public virtual ICollection<MRLResponse> MRLResponses { get; set; } = new List<MRLResponse>();

        public virtual ICollection<WsmHistoricalScore> HistoricalScores { get; set; } = new List<WsmHistoricalScore>();
    }
}
