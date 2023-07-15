using System.Text.Json.Serialization;

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

        [JsonPropertyName("id")]
        public int Id { get; init; }
        [JsonPropertyName("name")]
        public string Name { get; init; }
        [JsonPropertyName("notes")]
        public string Notes { get; init; }
    }
}
