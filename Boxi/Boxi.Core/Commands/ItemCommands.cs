using System.Text.Json.Serialization;
using Boxi.Core.DTOs;
using MediatR;

namespace Boxi.Core.Commands
{
    public record CreateItemCommand : IRequest<EntityAddedDto>
    {
        public CreateItemCommand()
        {

        }

        public CreateItemCommand(int boxId, string description) : this(boxId, description, null)
        {

        }

        public CreateItemCommand(int boxId, string description, string barcode)
        {
            BoxId = boxId;
            Description = description;
            Barcode = barcode;
        }

        [JsonPropertyName("boxid")] public int BoxId { get; init; }
        [JsonPropertyName("description")] public string Description { get; init; }
        [JsonPropertyName("barcode")] public string Barcode { get; init; }
    }

    public record UpdateItemCommand : IRequest<ItemDto>
    {
        public UpdateItemCommand() { }

        public UpdateItemCommand(int itemId, string description)
        {
            Id = itemId;
            Description = description;
        }

        [JsonPropertyName("description")]
        public string Description { get; init; }
        [JsonPropertyName("id")]
        public int Id { get; init; }
    }

    public record DeleteItemCommand : IRequest
    {
        public DeleteItemCommand(int itemId)
        {
            Id = itemId;
        }

        public int Id { get; init; }
    }
}