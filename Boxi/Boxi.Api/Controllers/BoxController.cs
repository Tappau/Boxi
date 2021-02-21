using System;
using System.Threading.Tasks;
using Boxi.Core;
using Boxi.Core.Commands;
using Boxi.Core.DTOs;
using Boxi.Core.Queries;
using Boxi.Dal.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Boxi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMediator _mediator;

        // GET: api/<BoxController>
        public BoxController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var boxes = await _unitOfWork.BoxRepo.GetAllAsync();
            var boxes = await _mediator.Send(new GetAllBoxesQuery());
            return Ok(boxes);
        }

        // GET api/<BoxController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //var box = await _unitOfWork.BoxRepo.GetAsync(id);
            var box = await _mediator.Send(new GetBoxByIdQuery{Id = id});
            if (box == null)
            {
                return NotFound();
            }

            return Ok(box);
        }

        // POST api/<BoxController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBoxCommand newBoxCommand)
        {
            if (newBoxCommand == null)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(newBoxCommand);
            
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
        }

        // PUT api/<BoxController>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateBoxCommand updateBoxCommand)
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
                Console.WriteLine(e);
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