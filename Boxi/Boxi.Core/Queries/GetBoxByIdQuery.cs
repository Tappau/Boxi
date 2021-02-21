using System.Text.Json.Serialization;
using Boxi.Core.DTOs;
using MediatR;

namespace Boxi.Core.Queries
{
    public record GetBoxByIdQuery : IRequest<BoxDto>
    {
        public GetBoxByIdQuery()
        {
        }

        [JsonConstructor]
        public GetBoxByIdQuery(int id)
        {
            Id = id;
        }

        [JsonPropertyName("id")]
        public int Id { get; init; }
    }
}