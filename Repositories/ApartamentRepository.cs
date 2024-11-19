using System;
using System.Collections.Generic;
using System.Linq;
using GarageTagManagement.Models;

namespace GarageTagManagement.Repositories
{
    public class ApartmentRepository{

        private readonly List<Apartment> _apartments = new List<Apartment>();

        public ApartmentRepository()
        {
            _apartments.Add(new Apartment
            {
                Id = 1,
                Name = "101",
                ActiveTags = new List<int> { 1, 2 },
            });

            _apartments.Add(new Apartment
            {
                Id = 2,
                Name = "102",
                ActiveTags = new List<int> { 3, 4 },
            });
        }

        public IEnumerable<Apartment> GetAll()
        {
            return _apartments;
        }

        public Apartment GetById(int id)
        {
            return _apartments.FirstOrDefault(a => a.Id == id)!;
        }

        public void Add(Apartment apartment)
        {
            apartment.Id = _apartments.Any() ? _apartments.Max(a => a.Id) + 1 : 1;
            _apartments.Add(apartment);
        }

        public void Update(Apartment apartment)
        {
            var existingApartment = _apartments.FirstOrDefault(a => a.Id == apartment.Id);
            if (existingApartment != null)
            {
                existingApartment.Name = apartment.Name;
                existingApartment.ActiveTags = apartment.ActiveTags;
            }
        }

        public void Delete(int id)
        {
            var apartment = _apartments.FirstOrDefault(a => a.Id == id);
            if (apartment != null)
            {
                _apartments.Remove(apartment);
            }
        }
    }

}