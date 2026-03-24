using System;
using System.ComponentModel.DataAnnotations;

namespace PointScore.Models.DTOs
{
    public class WsmWeightInputDto
    {
        [Required]
        [Range(0, 100)]
        public decimal TechnicalWeight { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal FunctionalWeight { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal ScheduleWeight { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal UserImpactWeight { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal CostWeight { get; set; }
    }
}
