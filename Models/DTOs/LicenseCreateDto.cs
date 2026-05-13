using System;
using System.ComponentModel.DataAnnotations;

namespace PointScore.Models.DTOs
{
    public class LicenseCreateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "License key must be between 10 and 100 characters.")]
        public string Key { get; set; } = string.Empty;

        [Required]
        public DateTime Expiration { get; set; }

        public string Status { get; set; } = "Active";

        public Guid? UserId { get; set; }
    }
}
