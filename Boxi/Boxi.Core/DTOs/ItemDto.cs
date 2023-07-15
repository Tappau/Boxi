using System.Text.Json.Serialization;

namespace Boxi.Core.DTOs
{
    public record ItemDto
    {
        public ItemDto()
        {
        }

        public ItemDto(int itemId, int boxId, string description) : this(itemId, boxId, description, null)
        {
        }

        public ItemDto(int itemId, int boxId, string description, string barcode)
        {
            Id = itemId;
            BoxId = boxId;
            Description = description;
            Barcode = barcode;
        }

        [JsonPropertyName("id")] public int Id { get; init; }
        [JsonPropertyName("boxid")] public int BoxId { get; init; }
        [JsonPropertyName("description")] public string Description { get; init; }
        [JsonPropertyName("barcode")] public string Barcode { get; init; }
    }
}