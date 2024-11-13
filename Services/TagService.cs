// `Services/TagService.cs`**: Contém a lógica de negócios, como as regras de distribuição para moradores e visitantes, e verificação da validade da tag.

using System;
using System.Collections.Generic;
using System.Linq;
using GarageTagManagement.Models;
using GarageTagManagement.Repositories;

namespace GarageTagManagement.Services
{
    public class TagService(TagRepository tagRepository)
    {
        private readonly TagRepository _tagRepository = tagRepository;

        public IEnumerable<Tag> GetAll()
        {
            return _tagRepository.GetAll();
        }

        public Tag GetById(int id)
        {
            return _tagRepository.GetById(id);
        }

        public Tag GetByApartment(string id)
        {
            return _tagRepository.GetByApartment(id);
        }

        public void Add(Tag tag)
        {
            _tagRepository.Add(tag);
        }

        public void Update(Tag tag)
        {
            _tagRepository.Update(tag);
        }

        public void Delete(int id)
        {
            _tagRepository.Delete(id);
        }

        public bool IsTagValid(Tag tag)
        {
            return tag.ValidadeTag >= DateTime.Now;
        }
    }
}