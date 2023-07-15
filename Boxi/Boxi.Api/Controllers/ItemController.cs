using System.Threading.Tasks;
using Boxi.Core.Commands;
using Boxi.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Boxi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetAllItemsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _mediator.Send(new GetItemByIdQuery(id));
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateItemCommand createItemCommand)
        {
            if (createItemCommand == null)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(createItemCommand);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateItemCommand updateItemCommand)
        {
            if (updateItemCommand == null)
            {
                return BadRequest();
            }

            var item = await _mediator.Send(updateItemCommand);
            return Ok(item);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteItemCommand deleteItemCommand)
        {
            return await Delete(deleteItemCommand.Id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            await _mediator.Send(new DeleteItemCommand(id));
            return NoContent();
        }
    }
}
