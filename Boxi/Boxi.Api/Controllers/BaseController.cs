using Boxi.Dal.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Boxi.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        public BaseController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }
    }
}