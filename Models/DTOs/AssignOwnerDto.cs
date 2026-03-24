using System.ComponentModel.DataAnnotations;

namespace PointScore.Models.DTOs
{
    /// <summary>
    /// DTO para asignar un WSM Owner (según formulario oficial GMC – sección "WSM Assigned WSM Owner")
    /// </summary>
    public class AssignWsmOwnerDto
    {
        [Required(ErrorMessage = "WSM Id is required")]
        public Guid WsmId { get; set; }

        public Guid? OwnerId { get; set; }      // Opcional: asigna WSM Owner
    }

    /// <summary>
    /// Respuesta oficial tras asignar WSM Owner – refleja campos del formulario GMC
    /// </summary>
    public class AssignOwnerResponseDto
    {
        public Guid WsmId { get; set; }
        public string RequestNumber { get; set; } = null!;
        public string Title { get; set; } = null!;

        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; } = null!;
        public string OwnerEmail { get; set; } = null!;
        public string? OwnerOrganization { get; set; }

        public DateTime AssignedDate { get; set; }
        public DateTime WorkflowStatusDate { get; set; }

        public string Status => "In Progress";
        public string WorkflowStatus => "initiated";

        public string Message => "WSM Owner assigned successfully. Approval workflow officially initiated per GMC/DoD standard.";
    }
    public class WsmPersonalDto
{
    public Guid Id { get; set; }
    public string RequestNumber { get; set; } = null!;
    public string SystemName { get; set; } = null!;
    public string Title { get; set; } = null!;
    public DateTime DateReceived { get; set; }
    public DateTime? DateSubmitted { get; set; }

    public string Status { get; set; } = null!;
    public int OriScore { get; set; }

    public string AssignedWsmOwner { get; set; } = null!;
    public string? AssignedWsmOwnerEmail { get; set; }
    public string AssignedBlockOwner { get; set; } = null!;
    public string? AssignedBlockOwnerEmail { get; set; }

    public DateTime? AssignedDate { get; set; }
    public DateTime? DesiredNeedDate { get; set; }
    public DateTime? RequiredNeedDate { get; set; }

    public string DevCCB_Status { get; set; } = null!;
    public string ProdCCB_Status { get; set; } = null!;
}
}