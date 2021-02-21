using System;
using System.Threading;
using System.Threading.Tasks;
using Boxi.Core.Commands;
using Boxi.Core.DTOs;
using Boxi.Dal.Interfaces;
using MediatR;

namespace Boxi.Service.Handlers.CommandHandlers
{
    public class UpdateBoxCommandHandler : IRequestHandler<UpdateBoxCommand, BoxDto>
    {
        private readonly IUnitOfWork _unitofWork;

        public UpdateBoxCommandHandler(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        
        public async Task<BoxDto> Handle(UpdateBoxCommand request, CancellationToken cancellationToken)
        {
            var boxToUpdate = await _unitofWork.BoxRepo.GetAsync(request.Id);
            if (boxToUpdate == null)
            {
                throw new Exception("Box not found, please ensure it is a valid Id");
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                boxToUpdate.BoxName = request.Name;    
            }

            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                boxToUpdate.Notes = request.Description;    
            }

            _unitofWork.BoxRepo.Update(boxToUpdate);
            await _unitofWork.SaveAsync();
            return new BoxDto(boxToUpdate.Id, boxToUpdate.BoxName, boxToUpdate.Notes);
        }
    }
}