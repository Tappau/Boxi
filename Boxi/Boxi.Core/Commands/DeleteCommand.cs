using MediatR;

namespace Boxi.Core.Commands
{
    public record DeleteBoxCommand : IRequest
    {
        public DeleteBoxCommand(int id)
        {
            Id = id;
        }

        public int Id { get; init; }
    }
}