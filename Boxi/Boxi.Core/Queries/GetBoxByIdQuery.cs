using System.Collections.Generic;
using System.Net;
using Boxi.Core.Domain;
using Boxi.Core.DTOs;
using MediatR;

namespace Boxi.Core.Queries
{
    public class GetBoxByIdQuery : IRequest<BoxDto>
    {
        public int Id { get; set; }
    }
}