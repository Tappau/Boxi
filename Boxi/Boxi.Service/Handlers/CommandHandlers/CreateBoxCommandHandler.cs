using System.Threading;
using System.Threading.Tasks;
using Boxi.Core.Commands;
using Boxi.Core.Domain;
using Boxi.Core.DTOs;
using Boxi.Dal.Interfaces;
using MediatR;

namespace Boxi.Service.Handlers.CommandHandlers
{
    public class CreateBoxCommandHandler : IRequestHandler<CreateBoxCommand, EntityAddedDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateBoxCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EntityAddedDto> Handle(CreateBoxCommand request, CancellationToken cancellationToken)
        {
            var newBox = new Box(request.Name, request.Description);
             await _unitOfWork.BoxRepo.AddAsync(newBox);
            await _unitOfWork.SaveAsync();
            return new EntityAddedDto(newBox.Id);
        }
    }
}