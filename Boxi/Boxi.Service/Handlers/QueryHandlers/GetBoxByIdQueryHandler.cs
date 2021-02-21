using System.Threading;
using System.Threading.Tasks;
using Boxi.Core.DTOs;
using Boxi.Core.Queries;
using Boxi.Dal.Interfaces;
using MediatR;

namespace Boxi.Service.Handlers.QueryHandlers
{
    public class GetBoxByIdQueryHandler : IRequestHandler<GetBoxByIdQuery, BoxDto>
    {
        private readonly IBoxRepository _boxRepo;

        public GetBoxByIdQueryHandler(IBoxRepository boxRepository)
        {
            _boxRepo = boxRepository;
        }

        public async Task<BoxDto> Handle(GetBoxByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _boxRepo.GetAsync(request.Id);

            return new BoxDto
            {
                Id = result.Id,
                Name = result.BoxName,
                Notes = result.Notes
            };
        }
    }
}