using System.Collections.Generic;
using GarageTagManagement.Models;
using GarageTagManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace GarageTagManagement.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase{
        private readonly ApartmentService _apartmentService;

        public ApartmentController(ApartmentService apartmentService){
            _apartmentService = apartmentService;
            _apartmentService.ApartmentAdded += OnApartmentAdded;
        }

        private void OnApartmentAdded(object? sender, ApartamentAddedEventArgs e){
            Console.WriteLine($"Apartamento adicionado: ID={e.ApartmentId}, Nome={e.Name}");
        }

        // GET: api/apartment
        [HttpGet]
        public ActionResult<IEnumerable<Apartment>> Get(){
            var apartments = _apartmentService.GetAll();
            if (apartments == null || apartments.Count() == 0){
                return NoContent();
            }
            return Ok(apartments);
        }

        // GET: api/apartment/{id}
        [HttpGet("{id}")]
        public ActionResult<Apartment> Get(int id){
            var apartment = _apartmentService.GetById(id);
            if (apartment == null){
                return NotFound();
            }
            return Ok(apartment);
        }

        // POST: api/apartment
        [HttpPost]
        public ActionResult Post([FromBody] Apartment apartment){
            _apartmentService.Add(apartment);
            return Ok();
        }

        // PUT: api/apartment/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Apartment apartment){
            if (id != apartment.Id){
                return BadRequest();
            }
            _apartmentService.Update(apartment);
            return Ok();
        }

        // DELETE: api/apartment/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id){
            _apartmentService.Delete(id);
            return Ok();
        }
    }
}