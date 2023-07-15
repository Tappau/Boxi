using System.Collections.Generic;
using Boxi.Core.DTOs;
using MediatR;

namespace Boxi.Core.Queries
{
    public record GetAllBoxesQuery : IRequest<List<BoxDto>>
    {

    }
}