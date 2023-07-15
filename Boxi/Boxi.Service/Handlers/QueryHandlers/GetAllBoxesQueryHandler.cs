using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Boxi.Core.DTOs;
using Boxi.Core.Queries;
using Boxi.Dal.Interfaces;
using MediatR;

namespace Boxi.Service.Handlers.QueryHandlers
{
    public class GetAllBoxesQueryHandler : IRequestHandler<GetAllBoxesQuery, List<BoxDto>>
    {
        private readonly IBoxRepository _boxRepo;

        public GetAllBoxesQueryHandler(IBoxRepository boxRepository)
        {
            _boxRepo = boxRepository;
        }

        public async Task<List<BoxDto>> Handle(GetAllBoxesQuery request, CancellationToken cancellationToken)
        {
            var boxes = await _boxRepo.GetAllAsync();
            return await Task.FromResult(boxes.Select(b => new BoxDto(b.Id, b.BoxName, b.Notes)).ToList());
        }
    }
}
