using System.Collections.Generic;
using Boxi.Core.DTOs;
using MediatR;

namespace Boxi.Core.Queries
{
    public class GetAllItemsQuery : IRequest<List<ItemDto>>
    {
    }

    public class GetItemByIdQuery : IRequest<ItemDto>
    {
        public GetItemByIdQuery(int itemId)
        {
            ItemId = itemId;
        }
        public int ItemId { get; set; }
    }
}