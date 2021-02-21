using System.Collections.Generic;
using Boxi.Core.DTOs;
using MediatR;

namespace Boxi.Core.Queries
{
    public class GetAllBoxesQuery : IRequest<List<BoxDto>>
    {
        
    }
}