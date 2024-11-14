using System.Collections.Generic;
using GarageTagManagement.Models;
using GarageTagManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace GarageTagManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly TagService _tagService;

        public TagsController(TagService tagService)
        {
            _tagService = tagService;
        }

        // GET: api/tags
        [HttpGet]
        public ActionResult<IEnumerable<Tag>> Get()
        {
            var tags = _tagService.GetAll();
            if (tags == null || tags.Count() == 0)
            {
                return NoContent(); 
            }
            return Ok(tags);
        }

        // GET: api/tags/{id}
        [HttpGet("{id}")]
        public ActionResult<Tag> Get(int id)
        {
            var tag = _tagService.GetById(id);
            if (tag == null)
            {
                return NotFound(); 
            }
            return Ok(tag); 
        }

        // GET: api/tags/apto/{idApartamento}
        [HttpGet("apto/{idApartamento}")]
        public ActionResult<Tag> GetByApartment(string idApartamento)
        {
            var tag = _tagService.GetByApartment(idApartamento);
            if (tag == null)
            {
                return NotFound(); 
            }
            return Ok(tag); 
        }

        // POST: api/tags
        [HttpPost]
        public IActionResult Post([FromBody] Tag tag)
        {
            if (tag == null)
            {
                return BadRequest("Tag não pode ser nula."); 
            }

            _tagService.Add(tag);
            return CreatedAtAction(nameof(Get), new { id = tag.Id }, tag); 
        }

        // PUT: api/tags/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Tag tag)
        {
            if (tag == null)
            {
                return BadRequest("Tag não pode ser nula."); 
            }

            var existingTag = _tagService.GetById(id);
            if (existingTag == null)
            {
                return NotFound(); 
            }

            tag.Id = id; 
            _tagService.Update(tag);

            return Ok(tag); 
        }

        // DELETE: api/tags/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tag = _tagService.GetById(id);
            if (tag == null)
            {
                return NotFound();
            }

            _tagService.Delete(id);
            return NoContent(); 
        }

        // PUT: api/tags/{id}/toggle
        [HttpPut("{id}/toggle")]
        public IActionResult ToggleIsActive(int id)
        {
            var tag = _tagService.GetById(id);
            if (tag == null)
            {
                return NotFound(); 
            }

            tag.IsActive = !tag.IsActive;
            _tagService.Update(tag);

            return Ok(tag); 
        }

        // GET: api/tags/{id}/isValid
        [HttpGet("{id}/isValid")]
        public IActionResult IsTagValid(int id)
        {
            var tag = _tagService.GetById(id);
            if (tag == null)
            {
                return NotFound(); 
            }

            return Ok(_tagService.IsTagValid(tag)); 
        }

        // GET: api/tags/apto/{idApartamento}/isValid
        [HttpGet("apto/{idApartamento}/isValid")]
        public IActionResult IsTagValidByApartment(string idApartamento)
        {
            var tag = _tagService.GetByApartment(idApartamento);
            if (tag == null)
            {
                return NotFound(); 
            }

            return Ok(_tagService.IsTagValid(tag)); 
        }

        // PUT: api/tags/{id}/toggleValid 
        [HttpPut("{id}/toggleValid")]
        public IActionResult ToggleValid(int id)
        {
            var tag = _tagService.GetById(id);
            if (tag == null)
            {
                return NotFound(); 
            }

            if (tag.ValidadeTag.HasValue)
            {
                tag.ValidadeTag = tag.ValidadeTag.Value.AddYears(1);
            }
            _tagService.Update(tag);

            return Ok(tag); 
        }
    }
}
