using Boxi.Core.DTOs;
using MediatR;

namespace Boxi.Core.Commands
{
    public record UpdateBoxCommand : IRequest<BoxDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}