using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Boxi.Core;
using Boxi.Core.Commands;
using Boxi.Dal.Interfaces;

namespace Boxi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxController : BaseController
    {
        // GET: api/<BoxController>
        public BoxController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var boxes = await UnitOfWork.BoxRepo.GetAllAsync();
            return Ok(boxes);
        }

        // GET api/<BoxController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var box = await UnitOfWork.BoxRepo.GetAsync(id);
            if (box == null)
            {
                return NotFound();
            }

            return Ok(box);
        }

        // POST api/<BoxController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBoxCommand createBoxCommand)
        {
            if (createBoxCommand == null)
            {
                return BadRequest();
            }

            //Only needing the ToEntity method tempary until CQRS Mediatr implementation
            await UnitOfWork.BoxRepo.AddAsync(createBoxCommand.ToEntity());
            await UnitOfWork.SaveAsync();
            //This will be better as will return the route to the resource in the return message,
            //but not yet as currently Add in repos don't return the new object.
            //return CreatedAtAction(nameof(Get), new {id = newBox.Id}, newBox);
            return Created("Get", createBoxCommand);
        }

        // PUT api/<BoxController>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateBoxCommand updateBoxCommand)
        {
            if (updateBoxCommand == null)
            {
                return BadRequest();
            }

            var currentBox = await UnitOfWork.BoxRepo.GetAsync(updateBoxCommand.Id);
            if (currentBox != null)
            {
                currentBox.BoxName = updateBoxCommand.Name;
                currentBox.Notes = updateBoxCommand.Notes;

                UnitOfWork.BoxRepo.Update(currentBox);
                await UnitOfWork.SaveAsync();
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        // DELETE api/<BoxController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
