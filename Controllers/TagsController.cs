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
                return NoContent(); // Retorna 204 se não houver tags
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
                return NotFound(); // Retorna 404 se a tag não for encontrada
            }
            return Ok(tag); // Retorna a tag com 200 OK
        }

        // GET: api/tags/apto/{idApartamento}
        [HttpGet("apto/{idApartamento}")]
        public ActionResult<Tag> GetByApartment(string idApartamento)
        {
            var tag = _tagService.GetByApartment(idApartamento);
            if (tag == null)
            {
                return NotFound(); // Retorna 404 se não encontrar a tag
            }
            return Ok(tag); // Retorna a tag com 200 OK
        }

        // POST: api/tags
        [HttpPost]
        public IActionResult Post([FromBody] Tag tag)
        {
            if (tag == null)
            {
                return BadRequest("Tag não pode ser nula."); // Verifica se a tag é válida
            }

            _tagService.Add(tag);
            return CreatedAtAction(nameof(Get), new { id = tag.Id }, tag); // Retorna 201 com a localização da tag criada
        }

        // PUT: api/tags/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Tag tag)
        {
            if (tag == null)
            {
                return BadRequest("Tag não pode ser nula."); // Verifica se a tag é válida
            }

            var existingTag = _tagService.GetById(id);
            if (existingTag == null)
            {
                return NotFound(); // Retorna 404 se não encontrar a tag
            }

            tag.Id = id; // Garante que o ID da tag seja o mesmo do parâmetro
            _tagService.Update(tag);
            return NoContent(); // Retorna 204 sem conteúdo
        }

        // DELETE: api/tags/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tag = _tagService.GetById(id);
            if (tag == null)
            {
                return NotFound(); // Retorna 404 se a tag não for encontrada
            }

            _tagService.Delete(id);
            return NoContent(); // Retorna 204 sem conteúdo
        }
    }
}
