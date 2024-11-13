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
        private readonly List<Tag> _tags = new List<Tag>();

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
            if (tag.ValidadeTag == null)
            {
                tag.ValidadeTag = DateTime.Now.AddYears(1);
            }

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


        public bool IsTagValidById(int id)
        {
            var tag = _tagRepository.GetById(id);
            return IsTagValid(tag);
        }

        public bool IsTagValidByApartment(string id)
        {
            var tag = _tagRepository.GetByApartment(id);
            return IsTagValid(tag);
        }

        public void ToggleIsActive(int id)
        {
            var tag = _tagRepository.GetById(id);
            tag.IsActive = !tag.IsActive;
            _tagRepository.Update(tag);
        }

        public void ToggleValidade(int id, int hours)
        {
            var tag = _tagRepository.GetById(id);
            if (tag.ValidadeTag.HasValue)
            {
                tag.ValidadeTag = tag.ValidadeTag.Value.AddHours(hours);
            }
            _tagRepository.Update(tag);
        }

    }
}