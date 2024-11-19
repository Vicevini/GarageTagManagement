// `Repositories/TagRepository.cs`**: Classe para armazenar e manipular os dados em memória. Implementa operações CRUD para as tags.

using System;
using System.Collections.Generic;
using System.Linq;
using GarageTagManagement.Models;

namespace GarageTagManagement.Repositories
{
    public class TagRepository
    {
        private readonly List<Tag> _tags = new List<Tag>();

        public TagRepository()
        {
            _tags.Add(new Tag
            {
                Id = 1,
                IdApartamento = 1,
                TipoTag = "Morador",
                ValidadeTag = DateTime.Now.AddYears(1),
                IsActive = true
            });

            _tags.Add(new Tag
            {
                Id = 2,
                IdApartamento = 2,
                TipoTag = "Visitante",
                ValidadeTag = DateTime.Now.AddHours(48),
                IsActive = true
            });
        }

        public IEnumerable<Tag> GetAll()
        {
            return _tags;
        }

        public Tag GetById(int id)
        {
            return _tags.FirstOrDefault(t => t.Id == id)!;
        }

        public Tag GetByApartment(int id)
        {
            return _tags.FirstOrDefault(t => t.IdApartamento == id)!;
        }

        public void Add(Tag tag)
        {
            tag.Id = _tags.Any() ? _tags.Max(t => t.Id) + 1 : 1;
            _tags.Add(tag);
        }

        public void Update(Tag tag)
        {
            var existingTag = _tags.FirstOrDefault(t => t.Id == tag.Id);
            if (existingTag != null)
            {
                existingTag.IdApartamento = tag.IdApartamento;
                existingTag.TipoTag = tag.TipoTag;
                existingTag.ValidadeTag = tag.ValidadeTag;
                existingTag.IsActive = tag.IsActive;
            }
        }

        public void Delete(int id)
        {
            var tag = _tags.FirstOrDefault(t => t.Id == id);
            if (tag != null)
            {
                _tags.Remove(tag);
            }
        }
    }
}