using System;
using System.Collections.Generic;
using GarageTagManagement.Models;
using GarageTagManagement.Repositories;

namespace GarageTagManagement.Services
{
    public class ApartamentAddedEventArgs : EventArgs
    {
        public int ApartmentId { get; }
        public string Name { get; }

        public ApartamentAddedEventArgs(int apartmentId, string name)
        {
            ApartmentId = apartmentId;
            Name = name;
        }
    }

    public class ApartmentService
    {
        private readonly ApartmentRepository _apartmentRepository;

        public event EventHandler<ApartamentAddedEventArgs>? ApartmentAdded;

        public ApartmentService(ApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public IEnumerable<Apartment> GetAll()
        {
            return _apartmentRepository.GetAll();
        }

        public Apartment GetById(int id)
        {
            return _apartmentRepository.GetById(id);
        }

        public void Add(Apartment apartment)
        {
            _apartmentRepository.Add(apartment);
        }

        public void Update(Apartment apartment)
        {
            _apartmentRepository.Update(apartment);
        }

        public void Delete(int id)
        {
            _apartmentRepository.Delete(id);
        }

        protected virtual void OnApartmentAdded(ApartamentAddedEventArgs e)
        {
            ApartmentAdded?.Invoke(this, e);
        }
    }
}