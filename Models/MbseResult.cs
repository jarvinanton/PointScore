using System;

namespace PointScore.Models;

public class DataResponseDto
{
    // Las entradas se agrupan en un "Data response"
    public decimal CostScore { get; set; }
    public decimal Metrics { get; set; }
}

public class MbseResult
{
    public int Id { get; set; }
    public string? DataRequest { get; set; }
    public decimal CostScore { get; set; }
    public decimal Metrics { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}