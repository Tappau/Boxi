using System.Text.Json.Serialization;
using Boxi.Core.DTOs;
using MediatR;

namespace Boxi.Core.Commands
{
    public record UpdateBoxCommand : IRequest<BoxDto>
    {
        public UpdateBoxCommand()
        {
        }

        [JsonConstructor]
        public UpdateBoxCommand(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}