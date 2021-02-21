using System;
using System.Threading.Tasks;
using Boxi.Core.Commands;
using Boxi.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Boxi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxController : BaseController
    {
        private readonly IMediator _mediator;
        
        public BoxController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var boxes = await _mediator.Send(new GetAllBoxesQuery());
            return Ok(boxes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var box = await _mediator.Send(new GetBoxByIdQuery{Id = id});
            if (box == null)
            {
                return NotFound();
            }

            return Ok(box);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(CreateBoxCommand newBoxCommand)
        {
            if (newBoxCommand == null)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(newBoxCommand);
            
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateBoxCommand updateBoxCommand)
        {
            if (updateBoxCommand == null)
            {
                return BadRequest();
            }

            try
            {
                var t = await _mediator.Send(updateBoxCommand);
                return Ok(t);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteBoxCommand deleteBoxCommand)
        {
            return await Delete(deleteBoxCommand.Id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            await _mediator.Send(new DeleteBoxCommand(id));
            return NoContent();
        }
    }
}