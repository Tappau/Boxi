using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Boxi.Core.Domain;
using Boxi.Core.DTOs;
using Boxi.Core.Queries;
using Boxi.Dal.Interfaces;
using MediatR;

namespace Boxi.Service.Handlers.QueryHandlers
{
    public class ItemQueryHandlers : IRequestHandler<GetAllItemsQuery, List<ItemDto>>,
        IRequestHandler<GetItemByIdQuery, ItemDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemQueryHandlers(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ItemDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _unitOfWork.ItemRepo.GetAllAsync();
            return await Task.FromResult(items.Select(i => new ItemDto(i.Id, i.BoxId, i.Description, i.Barcode))
                .ToList());
        }

        public async Task<ItemDto> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWork.ItemRepo.GetAsync(request.ItemId);

            return new ItemDto(item.Id, item.BoxId, item.Description, item.Barcode);
        }
    }
}