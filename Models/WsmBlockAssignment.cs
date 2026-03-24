// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
// using PointScore.Models;
// using PointScore.Models.Enums;


// public class WsmBlockAssignment
// {
//     public Guid WsmRequestId { get; set; }
//     public Guid BlockId { get; set; }

//     public BlockAssignmentStatus Status { get; set; } = BlockAssignmentStatus.Recommended;

//     public DateTime AssignedDate { get; set; } = DateTime.UtcNow;

//     // NAVEGACIÓN BIDIRECCIONAL
//     public WsmRequest WsmRequest { get; set; } = null!;
//     public Block Block { get; set; } = null!;
// }

// public enum BlockAssignmentStatus
// {
//     Recommended,   // "Recommended but not assigned"
//     Assigned,      // "Assigned but not approved"
//     Approved       // "Approved for block"
// }