using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointScore.Models
{
    public class License
    {
        public int Id { get; set; }
        
        public Guid? UserId { get; set; }
        
        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "License key must be between 10 and 100 characters.")]
        public string Key { get; set; } = string.Empty;

        [Required]
        public DateTime Expiration { get; set; }

        public int CurrentMonthRequests { get; set; } = 0;

        [Required]
        [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])$", ErrorMessage = "MonthKey must be in format yyyy-MM.")]
        public string MonthKey { get; set; } = DateTime.UtcNow.ToString("yyyy-MM");

        [Required]
        [RegularExpression("^(Active|active|Suspended|suspended)$", ErrorMessage = "Status deve ser Active o Suspended.")]
        public string Status { get; set; } = "Active";
    }
}