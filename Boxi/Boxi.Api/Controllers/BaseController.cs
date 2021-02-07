using Microsoft.AspNetCore.Mvc;
using Boxi.Dal.Interfaces;

namespace Boxi.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork => _unitOfWork;
    }
}
