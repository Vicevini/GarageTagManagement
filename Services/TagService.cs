using System;
using System.Collections.Generic;
using GarageTagManagement.Models;
using GarageTagManagement.Repositories;

namespace GarageTagManagement.Services
{
    public class TagAddedEventArgs : EventArgs
    {
        public int TagId { get; }
        public string IdApartamento { get; }
        public DateTime ValidadeTag { get; }

        public TagAddedEventArgs(int tagId, string apartmentId, DateTime validadeTag)
        {
            TagId = tagId;
            IdApartamento = apartmentId;
            ValidadeTag = validadeTag;
        }

    }

    public class TagUpdatedEventArgs : EventArgs
    {
        public int TagId { get; }
        public string IdApartamento { get; }
        public DateTime ValidadeTag { get; }

        public TagUpdatedEventArgs(int tagId, string apartmentId, DateTime validadeTag)
        {
            TagId = tagId;
            IdApartamento = apartmentId;
            ValidadeTag = validadeTag;
        }
    }

    public class TagDeletedEventArgs : EventArgs
    {
        public int TagId { get; }

        public TagDeletedEventArgs(int tagId)
        {
            TagId = tagId;
        }
    }

    

    public class TagService
    {
        private readonly TagRepository _tagRepository;

        public event EventHandler<TagAddedEventArgs>? TagAdded;

        public event EventHandler<TagUpdatedEventArgs>? TagUpdated;

        public event EventHandler<TagDeletedEventArgs>? TagDeleted;

        public TagService(TagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

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

            if (tag.IdApartamento != null)
            {
                OnTagAdded(new TagAddedEventArgs(tag.Id, tag.IdApartamento, tag.ValidadeTag.Value));
            }
        }

        public void Update(Tag tag)
        {
            _tagRepository.Update(tag);

            if (tag.IdApartamento != null)
            {
                if (tag.ValidadeTag.HasValue)
                {
                    onTagUpdated(new TagUpdatedEventArgs(tag.Id, tag.IdApartamento, tag.ValidadeTag.Value));
                }
            }
        }

        public void Delete(int id)
        {
            _tagRepository.Delete(id);
            OnTagDeleted(new TagDeletedEventArgs(id));

            TagDeletedEventArgs e = new TagDeletedEventArgs(id);
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

        protected virtual void OnTagAdded(TagAddedEventArgs e)
        {
            TagAdded?.Invoke(this, e);
        }

        protected virtual void onTagUpdated(TagUpdatedEventArgs e)
        {
            TagUpdated?.Invoke(this, e);
        }

        protected virtual void OnTagDeleted(TagDeletedEventArgs e)
        {
            TagDeleted?.Invoke(this, e);
        }
    }
}
