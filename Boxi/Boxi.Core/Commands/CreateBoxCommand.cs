using Boxi.Core.DTOs;
using MediatR;

namespace Boxi.Core.Commands
{
    public class CreateBoxCommand : IRequest<EntityAddedDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}