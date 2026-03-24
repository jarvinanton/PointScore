namespace PointScore.Models
{
    public class MRLSubThread
    {
        public int Id { get; set; }
        public string Thread { get; set; } = null!;
        public string SubThreadCode { get; set; } = null!;
        public string SubThreadName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DisplayOrder { get; set; }
    }
}
