using System;
using System.Threading;
using System.Threading.Tasks;
using Boxi.Core.Commands;
using Boxi.Dal.Interfaces;
using MediatR;

namespace Boxi.Service.Handlers.CommandHandlers
{
    public class DeleteBoxCommandHandler : IRequestHandler<DeleteBoxCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBoxCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteBoxCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.BoxRepo.Delete(request.Id);
            await _unitOfWork.SaveAsync();
        }
    }
}
