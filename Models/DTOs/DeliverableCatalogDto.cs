namespace PointScore.Models.DTOs
{
    public class DeliverableCatalogDto
    {
        public int Id { get; set; }
        public string DIDNumber { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Type { get; set; } = null!; // CDRL / Non-CDRL
        public string? WhenNeeded { get; set; }
        public string NeededForWSM { get; set; } = null!; // Y, N, or blank
        public string? FunctionalArea { get; set; } // Mantener para compatibilidad (nombres separados por /)
        public List<FunctionalAreaDto>? FunctionalAreas { get; set; } // Nueva propiedad con objetos completos
        public decimal? Complexity { get; set; }
        public decimal? ScheduleScore { get; set; }
        public decimal? TotalScore { get; set; }
        public bool IsApplicableWSM { get; set; }
        public decimal? ApplicableWSMScore { get; set; }
    }

    public class DeliverableCatalogFilterDto
    {
        public string? Type { get; set; } // CDRL, NON-CDRL
        public string? FunctionalArea { get; set; }
        public string? NeededForWSM { get; set; } // Y, N
        public bool? IsApplicableWSM { get; set; }
        public decimal? MinComplexity { get; set; }
        public decimal? MaxComplexity { get; set; }
        public decimal? MinScore { get; set; }
        public decimal? MaxScore { get; set; }
    }
}
