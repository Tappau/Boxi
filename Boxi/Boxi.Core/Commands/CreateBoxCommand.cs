using System.Text.Json.Serialization;
using Boxi.Core.DTOs;
using MediatR;

namespace Boxi.Core.Commands
{
    public record CreateBoxCommand : IRequest<EntityAddedDto>
    {
        public CreateBoxCommand()
        {
        }

        [JsonConstructor]
        public CreateBoxCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
