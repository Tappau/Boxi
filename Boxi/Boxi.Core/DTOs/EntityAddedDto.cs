using System.Text.Json.Serialization;

namespace Boxi.Core.DTOs
{
    public record EntityAddedDto
    {
        public EntityAddedDto(int id)
        {
            Id = id;
        }
        
        public int Id { get; init; }
    }
}