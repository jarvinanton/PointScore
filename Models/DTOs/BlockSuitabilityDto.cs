namespace PointScore.Models.DTOs
{
    public class BlockSuitabilityDto
    {
        public Guid BlockId { get; set; }
        public string BlockName { get; set; } = string.Empty;
        public int? CurrentCost { get; set; }
        public int? CurrentPoints { get; set; }
        public int? MaxCost { get; set; }
        public int? MaxPoints { get; set; }
        
        // Extra info to help debug/verify the random logic
        public int WsmEstimatedCost { get; set; }
        public int WsmEstimatedPoints { get; set; }
    }
}
