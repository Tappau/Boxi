using System;
using System.Threading;
using System.Threading.Tasks;
using Boxi.Core.Commands;
using Boxi.Core.Domain;
using Boxi.Core.DTOs;
using Boxi.Dal.Interfaces;
using MediatR;

namespace Boxi.Service.Handlers.CommandHandlers
{
    public class ItemCommandsHandlers : IRequestHandler<CreateItemCommand, EntityAddedDto>
    , IRequestHandler<UpdateItemCommand, ItemDto>, IRequestHandler<DeleteItemCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemCommandsHandlers(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EntityAddedDto> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var newItem = new Item(request.Description, request.Barcode) { BoxId = request.BoxId };
            await _unitOfWork.ItemRepo.AddAsync(newItem);
            await _unitOfWork.SaveAsync();
            return new EntityAddedDto(newItem.Id);
        }

        public async Task<ItemDto> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var itemToUpdate = await _unitOfWork.ItemRepo.GetAsync(request.Id);
            if (itemToUpdate == null)
            {
                throw new Exception("Item not found, please ensure Item Id provided is valid");
            }

            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                itemToUpdate.Description = request.Description;
            }

            _unitOfWork.ItemRepo.Update(itemToUpdate);
            await _unitOfWork.SaveAsync();
            return new ItemDto(itemToUpdate.Id, itemToUpdate.BoxId, itemToUpdate.Description);
        }

        public async Task Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.ItemRepo.Delete(request.Id);
            await _unitOfWork.SaveAsync();
        }
    }
}
