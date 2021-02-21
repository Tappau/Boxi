namespace Boxi.Core.DTOs
{
    public record BoxDto
    {
        public BoxDto()
        {
            
        }

        public BoxDto(int id, string name, string notes)
        {
            Id = id;
            Name = name;
            Notes = notes;
        }
        public int Id { get; init; }
        public string Name { get; init; }
        public string Notes { get; init; }
    }
}