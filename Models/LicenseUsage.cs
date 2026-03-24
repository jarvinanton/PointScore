using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models
{
    public class LicenseUsage
    {
        public int Id { get; set; }

        [Required]
        public int LicenseId { get; set; }

        [ForeignKey("LicenseId")]
        public License? License { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Usage count must be at least 1.")]
        public int Count { get; set; } = 1;
    }
}