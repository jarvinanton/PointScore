using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PointScore.Models;
using PointScore.Models.Enums;
namespace PointScore.Models{

public class ApprovalWorkflow
{
   [Key, ForeignKey(nameof(WsmRequest))]
    public Guid WsmRequestId { get; set; }

    public string WorkflowStatus { get; set; } = "initiated"; // initiated, paused, closed, approved

    // Dev CCB
    public bool DevCCB_Approved { get; set; }
    public DateTime? DevCCB_ApprovalDate { get; set; }
    public bool DevCCB_Disapproved { get; set; }
    public DateTime? DevCCB_DisapprovalDate { get; set; }

    // Prod CCB
    public bool ProdCCB_Approved { get; set; }
    public DateTime? ProdCCB_ApprovalDate { get; set; }
    public bool ProdCCB_Disapproved { get; set; }
    public DateTime? ProdCCB_DisapprovalDate { get; set; }

    // Fielding / Implementation CCB
    public bool FieldingCCB_Approved { get; set; }
    public DateTime? FieldingCCB_ApprovalDate { get; set; }

    public bool ImplementationNumberAssigned { get; set; }
    public string? ImplementationNumber { get; set; }
    public bool ImplementationCompleted { get; set; }
    public DateTime? ImplementationCompletedDate { get; set; }
    [MaxLength(1000)]
    public string? CommentsFollowOn { get; set; }
    public bool GovtApprovalSubmitted { get; set; }
    public DateTime? GovtApprovalSubmittedDate { get; set; }
    public string? GovtApprovalStatus { get; set; } // "Pending", "Approved", "Rejected"

    public bool? FlightTestRequired { get; set; }  // null = pendiente, true = Sí, false = No
    public DateTime? FlightTestDeterminationDate { get; set; }
    public string? FlightTestComments { get; set; }

    public bool? MaterialReleaseRequired { get; set; }  // null = pendiente, true = Sí, false = No
    public DateTime? MaterialReleaseDeterminationDate { get; set; }
    public string? MaterialReleaseComments { get; set; }  // Obligatorio si rechazan
    public string? MaterialReleaseDeterminedBy { get; set; }

    public string? ImpactThreshold { get; set; } // High, Medium, Low, None
    public DateTime? ImpactThresholdDate { get; set; }
    public string? ImpactThresholdComments { get; set; }

    public WsmRequest WsmRequest { get; set; } = null!;
}
}