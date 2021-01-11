using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boxi.Core.ApiResponses;
using Boxi.Dal.Interfaces;
using Boxi.Dal.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Boxi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PublisherController(IUnitOfWork publisherRepository)
        {
            _unitOfWork = publisherRepository;
        }

        // GET: api/<PublisherController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var publishers = await _unitOfWork.PublisherRepo.GetAllAsync();
            return Ok(publishers);
        }

        // GET api/<PublisherController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var publisher = await _unitOfWork.PublisherRepo.GetAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            return Ok(publisher);
        }

        // POST api/<PublisherController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Publisher newPublisher)
        {
            if (newPublisher == null)
            {
                return BadRequest();
            }

            await _unitOfWork.PublisherRepo.AddAsync(newPublisher);
            await _unitOfWork.SaveAsync();
            return Created("GetPublisher", newPublisher);
        }

        // PUT api/<PublisherController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PublisherController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }

    public static class Map
    {
        public static PublisherResponse ToResponse(this Publisher pub)
        {
            return new PublisherResponse()
            {
                Id = pub.PublisherId,
                Name = pub.PubName,
                Notes = pub.Notes,
                Url = pub.Url,
                YearBegan = pub.YearBegan
            };
        }
    }
}
