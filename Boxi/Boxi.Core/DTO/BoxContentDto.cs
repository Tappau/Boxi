namespace Boxi.Core.DTO
{
    public class BoxContentDto
    {
        public int IssueId { get; set; }
        public string Publisher { get; set; }
        public string SeriesName { get; set; }
        public string IssueNumber { get; set; }
        public int Qty { get; set; }
        public string Condition { get; set; }
        public int GradeId { get; set; }
    }
}