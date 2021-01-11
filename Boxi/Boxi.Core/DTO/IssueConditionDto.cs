namespace Boxi.Core.DTO
{
    public class IssueConditionDto
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public byte GradeId { get; set; }
        public int Quantity { get; set; }
        public string GradeName { get; set; }
    }
}